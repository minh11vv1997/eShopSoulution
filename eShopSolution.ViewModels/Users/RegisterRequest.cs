using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eShopSolution.ViewModels.Users
{
    public class RegisterRequest
    {
        [Display(Name = "Tên")]
        [Required(ErrorMessage = "Phải nhập {0}")]
        public string FirstName { get; set; }

        [Display(Name = "Họ")]
        [Required(ErrorMessage = "Phải nhập {0}")]
        public string LastName { get; set; }

        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Phải nhập {0}")]
        //[RegularExpression(@"^[A-Za-z0-9]+@([a-zA-Z]+\\.)+[a-zA-Z]{2,6}]&", ErrorMessage = "Email Không hợp lệ")]
        public string Email { get; set; }

        [Display(Name = "Số điện thoại")]
        [Phone(ErrorMessage = "Phải là số điện thoại")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Tài khoản")]
        [Required(ErrorMessage = "Phải nhập {0}")]
        public string UserName { get; set; }

        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Phải nhập {0}")]
        [StringLength(255, ErrorMessage = "Phải nhập từ 5 kí tự trở lên", MinimumLength = 5)]
        public string Password { get; set; }

        [Display(Name = "Xác nhận mật khẩu")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Phải nhập mật khẩu")]
        public string ComfirmPassword { get; set; }
    }
}