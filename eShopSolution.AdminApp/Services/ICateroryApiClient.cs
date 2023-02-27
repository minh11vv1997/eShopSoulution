using eShopSolution.ViewModels.CategoryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Services
{
    public interface ICateroryApiClient
    {
        Task<List<CategoryViewModel>> GetAllCategory(string languageId);
    }
}