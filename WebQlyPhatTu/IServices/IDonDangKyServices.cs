using WebQlyPhatTu.Dto;
using WebQlyPhatTu.Helper;
using WebQlyPhatTu.Models;

namespace WebQlyPhatTu.IServices
{
    public interface IDonDangKyServices
    {
        ReturnObject<DonDangKy> GuiDon(int phattuid, DonDangKyDto dto);
        ReturnObject<DonDangKy> XacNhanDon(int userid, XacNhanDonDto dto);
        IQueryable<DaoTrang> GetDaoTrangByNTT(int nguoitrutri);
        IQueryable<DonDangKy> GetDonDangKyByDT(int daotrangid);
    }
}
