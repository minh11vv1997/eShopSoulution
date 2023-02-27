using eShopSolution.AdminApp.Services;
using eShopSolution.ViewModels.ProductModels;
using eShopSoulution.Utilities.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClinet;
        private readonly ICateroryApiClient _cateroryApiClient;
        private readonly IConfiguration _configuration;

        public ProductController(IProductApiClient productApiClinet,
            ICateroryApiClient cateroryApiClient,
            IConfiguration configuration)
        {
            _productApiClinet = productApiClinet;
            _cateroryApiClient = cateroryApiClient;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index(string keyword, int? categoryId, int pageIndex = 1, int pageSize = 10)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);
            var requestProduct = new GetManageProductPagingRequest()
            {
                Keyword = keyword,
                CateogoryId = categoryId,
                PageIndex = pageIndex,
                PageSize = pageSize,
                LanguageId = languageId,
            };
            ViewData["Keyword"] = keyword;

            //Truyền List Category
            var categories = await _cateroryApiClient.GetAllCategory(languageId);
            ViewBag.Categories = categories.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            if (TempData["result"] != null)
            {
                ViewBag.SuccessMSg = TempData["result"];
            }
            var data = await _productApiClinet.GetPagingProduct(requestProduct);
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var result = await _productApiClinet.CreateProduct(request);
            if (result)
            {
                TempData["result"] = "Thêm mới sản phẩm thành công";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Thêm sản phẩm thất bại");
            return View(result);
        }
    }
}