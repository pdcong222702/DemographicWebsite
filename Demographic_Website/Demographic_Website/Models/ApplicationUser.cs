using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Demographic_Website.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Bạn chưa nhập CCCD")]
        [MinLength(12, ErrorMessage = "CCCD dài tối thiểu 12 ký tự")]
        public string? IdentificationCode { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có độ dài tối tiểu 6 ký tự")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập lại mật khẩu")]
        [Compare("Password", ErrorMessage = "Nhập lại mật khẩu không giống mật khẩu đã nhập")]
        [MinLength(6, ErrorMessage = "Nhập lại mật khẩu phải có độ dài tối tiểu 6 ký tự")]
        public string? ConfirmPassword { get; set; }
        public bool? IsActived { get; set; }
    }
}
