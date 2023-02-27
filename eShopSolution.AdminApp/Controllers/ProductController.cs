using eShopSolution.AdminApp.Services;
using eShopSolution.ViewModels.ProductModels;
using eShopSoulution.Utilities.Constants;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClinet;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public ProductController(IProductApiClient productApiClinet,
            IWebHostEnvironment environment,
            IConfiguration configuration)
        {
            _productApiClinet = productApiClinet;
            _environment = environment;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);
            var request = new GetManageProductPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                LanguageId = languageId
            };
            ViewData["Keyword"] = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMSg = TempData["result"];
            }
            var data = await _productApiClinet.GetUserPagingProduct(request);
            return View(data.ResultObj);
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

        [HttpPost]
        public ActionResult UpLoadImage(List<IFormFile> file)
        {
            var filePath = "";
            foreach (IFormFile photo in Request.Form.Files)
            {
                string serverMapPath = Path.Combine(_environment.WebRootPath, "Images", photo.FileName);
                using (var stream = new FileStream(serverMapPath, FileMode.Create))
                {
                    photo.CopyTo(stream);
                }
                filePath = "https://localhost:44351" + "/Images/" + photo.FileName;
            }
            return Json(new { url = filePath });
        }
    }
}