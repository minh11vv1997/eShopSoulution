using eShopSolution.ApiIntegration;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICateroryApiClient _cateroryApiClient;

        public ProductController(ICateroryApiClient cateroryApiClient)
        {
            // _cateroryApiClient = cateroryApiClient.GetAllCategory();
        }

        public IActionResult Detail(int id)
        {
            return View();
        }

        public IActionResult Category(int id)
        {
            // var products =
            return View();
        }
    }
}