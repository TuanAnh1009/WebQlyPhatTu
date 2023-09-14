using System.Text.Json.Serialization;

namespace WebQlyPhatTu.Models
{
    public class PhatTu
    {
        public int PhatTuId { get; set; }
        public string? AnhChup { get; set; }
        public Boolean? DaHoanTuc { get; set; }
        public string Email { get; set; }
        public int? GioiTinh { get; set; }
        public string? Ho { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        public DateTime? NgayHoanTuc { get; set; }
        public DateTime? NgaySinh { get; set; }
        public DateTime? NgayXuatGia { get; set; }
        [JsonIgnore]
        public string PassWord { get; set; }
        public string? PhapDanh { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Ten { get; set; }
        public string? TenDem { get; set; }
        public int? ChuaId { get; set; }
        public Chua? Chua { get; set; }
        public int? KieuThanhVienId { get; set; }
        public KieuThanhVien? KieuThanhVien { get; set; }
        public IEnumerable<DonDangKy> Dondangkys { get; set; }
        public IEnumerable<PhatTuDaoTrang> Phattudaotrangs { get; set; }
        public UserRoles UserRoles { get; set; }
        public bool IsActive { get; set; }
        public PhatTu()
        {
            NgayCapNhat = DateTime.UtcNow;
            IsActive = true;
            DaHoanTuc = false;
        }
    }
}
