using System.ComponentModel.DataAnnotations;

namespace WebQlyPhatTu.Dto
{
    public class Login
    {
        [Required(ErrorMessage = "Email hoặc mật khẩu không được để trống.")]
        public string Email { get; set; }

        [Required]
        public string PassWord { get; set; }
    }
}
