using eShopSolution.ViewModels.CommentDto;
using eShopSolution.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Services
{
    public interface IUserApiClient
    {
        Task<ApiResult<string>> Authenticate(LoginRequest request);

        Task<ApiResult<PagedResult<UserViewModel>>> GetUserPaging(GetUserPagingRequest request);

        Task<ApiResult<bool>> AddRegisterUser(RegisterRequest registerRequest);

        Task<ApiResult<bool>> UpdateRegisterUser(Guid id, UserUpdateRequest requestUp);

        Task<ApiResult<UserViewModel>> GetByIdUser(Guid id);

        Task<ApiResult<bool>> Delete(Guid id);

        Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);
    }
}