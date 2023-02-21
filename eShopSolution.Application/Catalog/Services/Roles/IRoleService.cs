using eShopSolution.ViewModels.RoleViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Services.Roles
{
    public interface IRoleService
    {
        Task<List<RoleVm>> GetAllRole();
    }
}