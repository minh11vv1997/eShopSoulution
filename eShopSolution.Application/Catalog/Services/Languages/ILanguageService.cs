using eShopSolution.ViewModels.CommentDto;
using eShopSolution.ViewModels.LanguageModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Services.Languages
{
    public interface ILanguageService
    {
        Task<ApiResult<List<LanguageVm>>> GetLangue();
    }
}