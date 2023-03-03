using eShopSolution.ApiIntegration;
using eShopSolution.WebApp.Models;
using eShopSoulution.Utilities.Constants;
using LazZiya.ExpressLocalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISharedCultureLocalizer _loc;
        private readonly ISlideApiClient _slideApiClient;
        private readonly IProductApiClient _productApiClient;

        public HomeController(ILogger<HomeController> logger,
            ISlideApiClient slideApiClient,
            IProductApiClient productApiClient,
            ISharedCultureLocalizer loc)
        {
            _logger = logger;
            _productApiClient = productApiClient;
            _slideApiClient = slideApiClient;
            _loc = loc;
        }

        public async Task<IActionResult> Index()
        {
            var culture = CultureInfo.CurrentCulture.Name;
            var slide = await _slideApiClient.GetAllSlide();
            var featureProduct = await _productApiClient.GetFeaturedProducts(culture, SystemConstants.NUMBER_FEATURE_PRODUCT);
            var listtipProduct = await _productApiClient.GetListTipdProducts(culture, SystemConstants.NUMBER_LISTTIP_PRODUCT);
            var viewModel = new ParentViewModel()
            {
                SlideViewModel = slide,
                FeaturedProducts = featureProduct,
                ListTipProduct = listtipProduct
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region Phương thức sử  dụng hàm để sử dụng language

        public IActionResult SetCultureCookie(string cltr, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cltr)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        #endregion Phương thức sử  dụng hàm để sử dụng language
    }
}