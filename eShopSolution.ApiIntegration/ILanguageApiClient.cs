using eShopSolution.ViewModels.CommentDto;
using eShopSolution.ViewModels.LanguageModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.ApiIntegration
{
    public interface ILanguageApiClient
    {
        Task<ApiResult<List<LanguageVm>>> GetLangue();
    }
}