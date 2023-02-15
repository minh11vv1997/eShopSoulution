using eShopSolution.Data.Entities;
using eShopSolution.ViewModels.CommentDto;
using eShopSolution.ViewModels.Users;
using eShopSoulution.Utilities.Excentions;
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
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = singInManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<string> Authencate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                throw new EShopException("Cannot find user name");
            }
            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RemeberMe, true);
            if (!result.Succeeded)
            {
                return null;
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
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> Register(RegisterRequest request)
        {
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
                return true;
            }
            return false;
        }

        public async Task<PagedResult<UserViewModel>> GetUserPagging(GetUserPagingRequest request)
        {
            var query = _userManager.Users;
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.UserName.Contains(request.Keyword) || x.PhoneNumber.Contains(request.Keyword));
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
                                      Id = x.Id
                                  }).ToListAsync();
            // 4: Select and projection
            var pagedResult = new PagedResult<UserViewModel>()
            {
                TotaRecord = totalRow,
                Item = data
            };
            return pagedResult;
        }
    }
}