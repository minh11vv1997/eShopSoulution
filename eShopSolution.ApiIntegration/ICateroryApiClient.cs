using eShopSolution.ViewModels.CategoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.ApiIntegration
{
    public interface ICateroryApiClient
    {
        Task<List<CategoryViewModel>> GetAllCategory(string languageId);
    }
}