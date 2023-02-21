using eShopSolution.Data.Entities;
using eShopSolution.ViewModels.CommentDto;
using eShopSolution.ViewModels.Users;
using eShopSoulution.Utilities.Excentions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Services.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _configuration;

        public UserService(UserManager<AppUser> userManager,
            SignInManager<AppUser> singInManager,
            RoleManager<AppRole> roleManager,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = singInManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<ApiResult<string>> Authencate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return new ApiErrorResult<string>("Tài khoản không tồn tại");
            }
            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RemeberMe, true);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<string>("Đăng nhập không đúng");
            }
            // Lấy ra claims để mã hóa cho user
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Role , string.Join(";",roles)),
                new Claim(ClaimTypes.Name,request.UserName)
            };
            // Mã hóa user bằng JWT
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Tokens:Issuer"],
                _configuration["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3), // thời gian hết hạn
                signingCredentials: creds);   //ký tá

            return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
        }

        public async Task<ApiResult<bool>> Register(RegisterRequest request)
        {
            var checkUser = await _userManager.FindByNameAsync(request.UserName);
            if (checkUser != null)
            {
                return new ApiErrorResult<bool>("User name đã tồn tại");
            }
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return new ApiErrorResult<bool>("Email đã tồn tại");
            }
            var user = new AppUser()
            {
                Dob = request.Dob,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Đăng ký không thành công");
        }

        public async Task<ApiResult<PagedResult<UserViewModel>>> GetUserPagging(GetUserPagingRequest request)
        {
            var query = _userManager.Users;
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.UserName.Contains(request.Keyword) || x.PhoneNumber.Contains(request.Keyword) || x.FirstName.Contains(request.Keyword));
            }
            // 3: Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                                  .Take(request.PageSize)
                                  .Select(x => new UserViewModel()
                                  {
                                      FirstName = x.FirstName,
                                      LastName = x.LastName,
                                      Email = x.Email,
                                      PhoneNumber = x.PhoneNumber,
                                      UserName = x.UserName,
                                      Dob = x.Dob,
                                      Id = x.Id
                                  }).ToListAsync();
            // 4: Select and projection
            var pagedResult = new PagedResult<UserViewModel>()
            {
                TotaRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Item = data
            };
            return new ApiSuccessResult<PagedResult<UserViewModel>>(pagedResult);
        }

        public async Task<ApiResult<bool>> EditUser(Guid id, UserUpdateRequest request)
        {
            var checkUserUpdate = _userManager.Users.Any(x => x.Email == request.Email && x.Id != id);
            if (checkUserUpdate)
            {
                return new ApiErrorResult<bool>("Email đã tồn tại");
            }
            var user = await _userManager.FindByIdAsync(id.ToString());
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Dob = request.Dob;
            user.Email = request.Email;
            user.Dob = request.Dob;
            user.PhoneNumber = request.PhoneNumber;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Cập nhật không thành công");
        }

        public async Task<ApiResult<UserViewModel>> GetByIdUser(Guid id)
        {
            var userId = await _userManager.FindByIdAsync(id.ToString());
            if (userId == null)
            {
                return new ApiErrorResult<UserViewModel>("User không tồn tại");
            }
            var roles = await _userManager.GetRolesAsync(userId);
            var userVM = new UserViewModel()
            {
                FirstName = userId.FirstName,
                LastName = userId.LastName,
                Email = userId.Email,
                Dob = userId.Dob,
                Id = userId.Id,
                PhoneNumber = userId.PhoneNumber,
                UserName = userId.UserName,
                Roles = roles
            };
            return new ApiSuccessResult<UserViewModel>(userVM);
        }

        public async Task<ApiResult<bool>> Delete(Guid id)
        {
            var userId = await _userManager.FindByIdAsync(id.ToString());
            if (userId == null)
            {
                return new ApiErrorResult<bool>("User không tồn tại");
            }
            var result = await _userManager.DeleteAsync(userId);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>();
        }

        public async Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request)
        {
            //B1 : Lấy ra user
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<bool>("Tài khoản không tồn tại");
            }
            var removeRoles = request.Roles.Where(x => x.Selected == false).Select(x => x.Name).ToList();
            foreach (var roleNameRemote in removeRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleNameRemote) == true)
                {
                    await _userManager.RemoveFromRoleAsync(user, roleNameRemote);
                }
            }
            await _userManager.RemoveFromRolesAsync(user, removeRoles);
            var addveRoles = request.Roles.Where(x => x.Selected).Select(x => x.Name).ToList();
            foreach (var roleName in addveRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == false)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }
            }
            return new ApiSuccessResult<bool>();
        }
    }
}