namespace WebQlyPhatTu.Models
{
    public class DonDangKy
    {
        public int DonDangKyId { get; set; }
        public DateTime NgayGuiDon { get; set; }
        public DateTime? NgayXuLy { get; set; }
        public int? NguoiXuLy { get; set; }
        public int TrangThaiDon { get; set; }
        public int DaoTrangId { get; set; }
        public DaoTrang? DaoTrang { get; set; }
        public int PhatTuId { get; set; }
        public PhatTu? PhatTu { get; set; }
        public DonDangKy()
        {
            NgayGuiDon = DateTime.UtcNow;
        }
    }
}
