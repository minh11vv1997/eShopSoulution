using eShopSolution.ViewModels.CommentDto;
using eShopSolution.ViewModels.RoleViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Services
{
    public interface IRoleApiClient
    {
        Task<ApiResult<List<RoleVm>>> GetAllRole();
    }
}