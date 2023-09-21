using WebQlyPhatTu.Dto;
using WebQlyPhatTu.Helper;
using WebQlyPhatTu.Models;

namespace WebQlyPhatTu.IServices
{
    public interface IDonDangKyServices
    {
        ReturnObject<DonDangKy> GuiDon(int phattuid, DonDangKyDto dto);
        ReturnObject<DonDangKy> XacNhanDon(int userid, XacNhanDonDto dto);
        IQueryable<DonDangKy> GetDonDangKy(string? noitochuc, DateTime? ngayguidon, int? trangthaidon);
        IQueryable<DonDangKy> GetDonDangKyByTruTri(int nguoitrutri, string? noitochuc, DateTime? ngayguidon, int? trangthaidon);
    }
}
