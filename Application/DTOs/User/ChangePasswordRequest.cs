using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.User
{
    public class ChangePasswordRequest
    {
        public string? OldPassword { get; set; }
        [MinLength(6, ErrorMessage = "Mật khẩu yêu cầu lớn hơn 6 kí tự")]
        public string Password { get; set; } = string.Empty;

        [Compare("Password", ErrorMessage = "Mật khẩu không khớp")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public int userId;
    }
}
