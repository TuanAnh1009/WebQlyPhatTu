namespace WebQlyPhatTu.Models
{
    public class Chua
    {
        public int ChuaId { get; set; }
        public DateTime? CapNhap { get; set; }
        public string? DiaChi { get; set; }
        public DateTime? NgayThanhLap { get; set; }
        public string? TenChua { get; set; }
        public int? TruTri { get; set; }
        public IEnumerable<PhatTu> Phattus { get; set; }
    }
}
