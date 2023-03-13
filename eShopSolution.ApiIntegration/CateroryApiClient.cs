using eShopSolution.ApiIntegration;
using eShopSolution.ViewModels.CategoryModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace eShopSolution.ApiIntegration
{
    public class CateroryApiClient : BaseApiClient, ICateroryApiClient
    {
        public CateroryApiClient(IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration) : base(httpClientFactory, httpContextAccessor, configuration)
        {
        }

        public async Task<List<CategoryViewModel>> GetAllCategory(string languageId)
        {
            var data = await base.GetListAsync<CategoryViewModel>($"/api/Categories?languageId=" + languageId);
            return data;
        }

        public async Task<CategoryViewModel> GetById(string languageId, int id)
        {
            var data = await base.GetAsync<CategoryViewModel>($"/api/Categories/{id}/{languageId}");
            return data;
        }
    }
}