using eShopSolution.Application.Catalog.Services.Users;
using eShopSolution.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous] //Chưa đăng nhập cũng có thể gọi được.
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultToken = await _userService.Authencate(request);
            if (string.IsNullOrEmpty(resultToken.ResultObj))
            {
                return BadRequest(resultToken);
            }
            return Ok(resultToken);
        }

        [HttpPost]
        [AllowAnonymous] //Chưa đăng nhập cũng có thể gọi được.
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultToken = await _userService.Register(request);
            if (!resultToken.IsSuccessed)
            {
                return BadRequest(resultToken);
            }
            return Ok(resultToken);
        }

        //PUT : https:localhos/api/users/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, [FromBody] UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultToken = await _userService.EditUser(id, request);
            if (!resultToken.IsSuccessed)
            {
                return BadRequest(resultToken);
            }
            return Ok(resultToken);
        }

        // https://localhost:44351/api/user/paging?pageInde=1&pageSize=10&Keyword=
        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetUserPagingRequest request)
        {
            var users = await _userService.GetUserPagging(request);
            return Ok(users);
        }

        //Get ByID để thực hiện update, delete
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var users = await _userService.GetByIdUser(id);  // trả về UserViewModel
            return Ok(users);
        }
    }
}