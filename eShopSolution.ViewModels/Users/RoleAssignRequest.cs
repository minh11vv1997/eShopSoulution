using eShopSolution.ViewModels.CommentDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.Users
{
    public class RoleAssignRequest
    {
        // Cần 1 danh sách roles
        public Guid Id { get; set; }

        public List<SelectItem> Roles { get; set; } = new List<SelectItem>();
    }
}