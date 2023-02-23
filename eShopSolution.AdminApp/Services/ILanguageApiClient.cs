using eShopSolution.ViewModels.CommentDto;
using eShopSolution.ViewModels.LanguageModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Services
{
    public interface ILanguageApiClient
    {
        Task<ApiResult<List<LanguageVm>>> GetLangue();
    }
}