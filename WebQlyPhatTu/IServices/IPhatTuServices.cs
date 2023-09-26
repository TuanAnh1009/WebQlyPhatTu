using WebQlyPhatTu.Dto;
using WebQlyPhatTu.Helper;
using WebQlyPhatTu.Models;

namespace WebQlyPhatTu.IServices
{
    public interface IPhatTuServices
    {
        IQueryable<PhatTu> DSPT(string? name, string? phapdanh, string? email, int? gioitinh);
        ReturnObject<PhatTu> AddPhatTu(AddPhatTuDto dto);
        ReturnObject<PhatTu> EditPhatTu(EditPhatTuDto dto);
        List<ObjList> ListChua();
        List<ObjList> ListKieuThanhVien();
        List<ObjList> ListTruTri();
    }
}
