using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebQlyPhatTu.Models;

namespace WebQlyPhatTu.Dto
{
    public class AddPhatTuDto
    {
        public int PhatTuId { get; set; }
        public Boolean? DaHoanTuc { get; set; }
        [Required]
        public string Email { get; set; }
        public int? GioiTinh { get; set; }
        public string? Ho { get; set; }
        public DateTime? NgayHoanTuc { get; set; }
        public DateTime? NgaySinh { get; set; }
        public DateTime? NgayXuatGia { get; set; }
        [Required]
        public string PassWord { get; set; }
        public string? PhapDanh { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Ten { get; set; }
        public string? TenDem { get; set; }
        public int? ChuaId { get; set; } = null;
        public int? KieuThanhVienId { get; set; } = null;
    }
}
