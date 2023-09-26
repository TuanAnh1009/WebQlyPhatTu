namespace WebQlyPhatTu.Dto
{
    public class UpdateUserDto
    {
        public int PhatTuId { get; set; }
        public string Email { get; set; }
        public int? GioiTinh { get; set; }
        public string? Ho { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? PhapDanh { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Ten { get; set; }
        public string? TenDem { get; set; }
        public int? ChuaId { get; set; } = null;
    }
}
