using eShopSolution.Application.Catalog.Services.Common;
using eShopSolution.Data.EF;
using eShopSolution.ViewModels.CategoryModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace eShopSolution.Application.Catalog.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly EShopDbContext _context;
        private readonly IStoregeService _storegeService;

        public CategoryService(EShopDbContext context, IStoregeService storegeService)
        {
            _context = context;
            _storegeService = storegeService;
        }

        public async Task<List<CategoryViewModel>> GetAll(string languageId)
        {
            // B1: Select lấy dữ liệu
            var query = from c in _context.Categories
                        join ct in _context.CategoryTranslations on c.Id equals ct.CategoryId
                        where ct.LanguageId == languageId
                        select new { c, ct };
            var data = await query.Select(x => new CategoryViewModel()
            {
                Id = x.c.Id,
                Name = x.ct.Name
            }).ToListAsync();
            return data;
        }
    }
}