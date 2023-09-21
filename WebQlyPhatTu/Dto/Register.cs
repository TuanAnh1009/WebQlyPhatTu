using System.ComponentModel.DataAnnotations;

namespace WebQlyPhatTu.Dto
{
    public class Register
    {
        [Required(ErrorMessage = "Họ không được để trống.")]
        public string? Ho { get; set; }
        public string? TenDem { get; set; }
        [Required(ErrorMessage = "Tên không được để trống.")]
        public string? Ten { get; set; }
        public DateTime? NgaySinh { get; set; }
        [Required(ErrorMessage = "Giới tính chưa được chọn.")]
        public int? GioiTinh { get; set; }
        [Required(ErrorMessage = "Email không được để trống.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        public string PassWord { get; set; }
    }
}
