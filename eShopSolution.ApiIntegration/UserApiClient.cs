using eShopSolution.ApiIntegration;
using eShopSolution.ViewModels.CommentDto;
using eShopSolution.ViewModels.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ApiIntegration
{
    public class UserApiClient : BaseApiClient, IUserApiClient
    {
        public UserApiClient(IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration) : base(httpClientFactory, httpContextAccessor, configuration)
        {
        }

        public async Task<ApiResult<bool>> AddRegisterUser(RegisterRequest registerRequest)
        {
            var reusult = await PostAsync<ApiResult<bool>>("/api/Users", registerRequest);
            return reusult;
        }

        public async Task<ApiResult<string>> Authenticate(LoginRequest request)
        {
            var result = await PostAsync<ApiResult<string>>("/api/Users/authenticate", request);
            return result;
        }

        public async Task<ApiResult<PagedResult<UserViewModel>>> GetUserPaging(GetUserPagingRequest request)
        {
            var result = await GetAsync<ApiResult<PagedResult<UserViewModel>>>(request);
            return result;
        }

        public async Task<ApiResult<bool>> UpdateRegisterUser(Guid id, UserUpdateRequest requestUp)
        {
            var result = await PutAsync<ApiResult<bool>>($"/api/Users/{id}", requestUp);
            return result;
        }

        public async Task<ApiResult<UserViewModel>> GetByIdUser(Guid id)
        {
            return await GetAsync<ApiResult<UserViewModel>>($"/api/Users/{id}");
        }

        public async Task<ApiResult<bool>> Delete(Guid id)
        {
            return await GetAsync<ApiResult<bool>>($"/api/Users/{id}");
        }

        public async Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request)
        {
            var result = await PutAsync<ApiResult<bool>>($"/api/Users/{id}/roles", request);
            return result;
        }
    }
}