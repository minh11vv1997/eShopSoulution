using eShopSolution.Application.Catalog.Services.Common;
using eShopSolution.Data.EF;
using eShopSolution.ViewModels.CommentDto;
using eShopSolution.ViewModels.LanguageModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Services.Languages
{
    public class LanguageService : ILanguageService
    {
        private readonly EShopDbContext _context;
        private readonly IStoregeService _storegeService;

        public LanguageService(EShopDbContext context, IStoregeService storegeService)
        {
            _context = context;
            _storegeService = storegeService;
        }

        public async Task<ApiResult<List<LanguageVm>>> GetLangue()
        {
            var languages = await _context.Languages.Select(x => new LanguageVm()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
            return new ApiSuccessResult<List<LanguageVm>>(languages);
        }
    }
}