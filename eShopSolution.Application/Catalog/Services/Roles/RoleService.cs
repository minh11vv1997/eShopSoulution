using eShopSolution.Data.Entities;
using eShopSolution.ViewModels.RoleViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Services.Roles
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _rolesManager;

        public RoleService(RoleManager<AppRole> rolesManager)
        {
            _rolesManager = rolesManager;
        }

        public async Task<List<RoleVm>> GetAllRole()
        {
            var roles = await _rolesManager.Roles.Select(x => new RoleVm()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToListAsync();

            return roles;
        }
    }
}