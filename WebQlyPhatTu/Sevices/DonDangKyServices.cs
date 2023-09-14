using WebQlyPhatTu.Dto;
using WebQlyPhatTu.Helper;
using WebQlyPhatTu.IServices;
using WebQlyPhatTu.Models;

namespace WebQlyPhatTu.Sevices
{
    public class DonDangKyServices : IDonDangKyServices
    {
        private readonly AppDbContext appDbContext;

        public DonDangKyServices()
        {
            appDbContext = new AppDbContext();
        }

        public IQueryable<DaoTrang> GetDaoTrangByNTT(int nguoitrutri)
        {
            var query = appDbContext.DaoTrang.Where(x => x.NguoiTruTri == nguoitrutri);
            return query;
        }

        public IQueryable<DonDangKy> GetDonDangKyByDT(int daotrangid)
        {
            var query = appDbContext.DonDangKy.Where(x => x.DaoTrangId == daotrangid).AsQueryable();
            return query;
        }

        public ReturnObject<DonDangKy> GuiDon(int phattuid, DonDangKyDto dto)
        {
            try
            {
                if (!appDbContext.DaoTrang.Any(x => x.DaoTrangId == dto.DaoTrangId))
                {
                    throw new Exception("Dao trang not exist");
                }
                if (appDbContext.DaoTrang.Any(x => x.DaoTrangId == dto.DaoTrangId && x.NguoiTruTri == phattuid))
                {
                    throw new Exception("Ban da la tru tri cua dao trang nay, khong the dang ky");
                }
                var dangKy = new DonDangKy()
                {
                    PhatTuId = phattuid,
                    DaoTrangId = dto.DaoTrangId,
                    TrangThaiDon = 2
                };
                appDbContext.Add(dangKy);
                appDbContext.SaveChanges();
                return new ReturnObject<DonDangKy>
                {
                    Mes = "Sent success",
                    Data = dangKy
                };
            }
            catch (Exception ex)
            {
                return new ReturnObject<DonDangKy>
                {
                    Mes = ex.Message,
                    Data = null,
                    Error = true
                };
            }
        }

        public ReturnObject<DonDangKy> XacNhanDon(int userid, XacNhanDonDto dto)
        {
            try
            {
                var xacnhandon = appDbContext.DonDangKy.FirstOrDefault(x => x.DonDangKyId == dto.DonDangKyId);
                if (xacnhandon == null)
                {
                    throw new Exception("Not exist");
                }
                xacnhandon.NgayXuLy = DateTime.UtcNow;
                xacnhandon.NguoiXuLy = userid;
                xacnhandon.TrangThaiDon = dto.TrangThaiDon;
                var daotrang = appDbContext.DaoTrang.FirstOrDefault(x => x.DaoTrangId == xacnhandon.DaoTrangId);
                if (xacnhandon.TrangThaiDon == 1)
                {
                    daotrang.SoThanhVienThamGia += 1;
                }
                appDbContext.Update(xacnhandon);
                appDbContext.Update(daotrang);
                appDbContext.SaveChanges();
                return new ReturnObject<DonDangKy>
                {
                    Mes = "success",
                    Data = xacnhandon
                };
            }
            catch (Exception ex)
            {
                return new ReturnObject<DonDangKy>
                {
                    Mes = ex.Message,
                    Data = null,
                    Error = true
                };
            }
        }
    }
}
