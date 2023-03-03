using eShopSolution.ApiIntegration;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.WebApp.Components
{
    public class SidebarViewComponent : ViewComponent
    {
        private readonly ICateroryApiClient _cateroryApiClient;

        public SidebarViewComponent(ICateroryApiClient cateroryApiClient)
        {
            _cateroryApiClient = cateroryApiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var item = await _cateroryApiClient.GetAllCategory(CultureInfo.CurrentCulture.Name);
            return View("Default", item);
        }
    }
}