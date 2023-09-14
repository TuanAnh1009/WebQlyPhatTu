using System.ComponentModel.DataAnnotations.Schema;

namespace WebQlyPhatTu.Models
{
    public class DaoTrang
    {
        public int DaoTrangId { get; set; }
        public Boolean? DaKetThuc { get; set; }
        public string? NoiDung { get; set; }
        public string? NoiToChuc { get; set; }
        public int SoThanhVienThamGia { get; set; }
        public int NguoiTruTri { get; set; }
        public IEnumerable<DonDangKy> LstDondangkys { get; set; }
        public IEnumerable<PhatTuDaoTrang> LstPhattus { get; set; }
        public DaoTrang() 
        {
            DaKetThuc = false;
            SoThanhVienThamGia = 0;
        }
    }
}
