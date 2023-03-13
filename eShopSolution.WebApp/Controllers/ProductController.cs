using eShopSolution.ApiIntegration;
using eShopSolution.ViewModels.ProductModels;
using eShopSolution.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly ICateroryApiClient _cateroryApiClient;

        public ProductController(IProductApiClient productApiClient, ICateroryApiClient cateroryApiClient)
        {
            _productApiClient = productApiClient;
            _cateroryApiClient = cateroryApiClient;
        }

        public async Task<IActionResult> Detail(int id, string culture)
        {
            var product = await _productApiClient.GetByIdCategory(id, culture);
            var model = new ProductDetailViewModel()
            {
                Products = product,
                //Category = await _cateroryApiClient.GetById(culture, product.Categories),
                ReLatedProducts = await _productApiClient.GetListReLatedProduct(culture, id),
                ProductImages = await _productApiClient.GetProductImages(product.Id, id)
            };

            return View(model);
        }

        public async Task<IActionResult> Category(int id, string culture, int page = 1, string keyWord = "")
        {
            var products = await _productApiClient.GetPagingProduct(new GetManageProductPagingRequest()
            {
                CategoryId = 1,
                PageIndex = page,
                PageSize = 10,
                LanguageId = culture,
                Keyword = keyWord
            });
            var model = new ProductCategoryViewModel()
            {
                Category = await _cateroryApiClient.GetById(culture, id),
                Products = products
            };
            return View(model);
        }
    }
}