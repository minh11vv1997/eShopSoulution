using eShopSolution.ApiIntegration;
using eShopSolution.ViewModels.CommentDto;
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
                CategoryId = categoryId,
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
                Value = x.Id.ToString(),
                Selected = categoryId.HasValue && categoryId.Value == x.Id
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

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);
            var productId = await _productApiClinet.GetByIdCategory(id, languageId);
            var updateUpdate = new ProductUpdateRequest()
            {
                Id = productId.Id,
                Description = productId.Description,
                Details = productId.Details,
                Name = productId.Name,
                SeoTitle = productId.SeoTitle,
                SeoAlias = productId.SeoAlias,
                SeoDescription = productId.SeoDescription
            };
            return View(updateUpdate);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var result = await _productApiClinet.UpdateProduct(request);
            if (result)
            {
                TempData["result"] = "Cập nhật sản phẩm thành công";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Cập nhật phẩm thất bại");
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> CategoryAssign(int id)
        {
            var CategoryAssignRequest = await GetCategoryAssignRequest(id);
            return View(CategoryAssignRequest);
        }

        [HttpPost]
        public async Task<IActionResult> CategoryAssign(CategoryAssignRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var result = await _productApiClinet.CategoryAssign(request.Id, request);
            if (result)
            {
                TempData["result"] = "Cập nhật danh mục thành công";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Cập nhật danh mục thất bại");
            var roleAssignRequest = await GetCategoryAssignRequest(request.Id);

            return View(roleAssignRequest);
        }

        private async Task<CategoryAssignRequest> GetCategoryAssignRequest(int id)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);
            var productObj = await _productApiClinet.GetByIdCategory(id, languageId);
            var categories = await _cateroryApiClient.GetAllCategory(languageId);
            var categoryAssignRequest = new CategoryAssignRequest();
            foreach (var role in categories)
            {
                categoryAssignRequest.Categories.Add(new SelectItem()
                {
                    Id = role.Id.ToString(),
                    Name = role.Name,
                    Selected = productObj.Categories.Contains(role.Name)
                });
            }
            return categoryAssignRequest;
        }
    }
}