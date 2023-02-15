using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.Users
{
    public class UserViewModel
    {
        // Chứa các thông tin từ user mà muốn lấy
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
    }
}