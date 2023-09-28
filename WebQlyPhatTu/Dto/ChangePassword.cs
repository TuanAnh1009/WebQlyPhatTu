using System.ComponentModel.DataAnnotations;

namespace WebQlyPhatTu.Dto
{
    public class ChangePassword
    {
        [Required(ErrorMessage = "Mật khẩu cũ không được để trống.")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Mật khẩu mới không được để trống.")]
        public string NewPassword { get; set; }
        [Required]
        public string ReNewPassword { get; set; }
    }
}
