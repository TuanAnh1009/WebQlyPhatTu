using WebQlyPhatTu.Models;

namespace WebQlyPhatTu.Helper
{
    public class ObjPhatTu
    {
        public PageResult<PhatTu> ListPhatTu { get; set; }
        public List<ObjList>? ListChua { get; set; }
        public List<KieuThanhVien>? ListKieuThanhVien { get; set; }
    }
}
