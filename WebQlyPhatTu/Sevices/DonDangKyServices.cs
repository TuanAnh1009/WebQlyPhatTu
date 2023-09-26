using Microsoft.EntityFrameworkCore;
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

        public IQueryable<DonDangKy> GetDonDangKy(string? noitochuc, DateTime? ngayguidon, int? trangthaidon)
        {
            var query = appDbContext.DonDangKy.Include(x => x.PhatTu).Include(y => y.DaoTrang).AsQueryable();
            if(!string.IsNullOrWhiteSpace(noitochuc) )
            {
                query = query.Where(x => x.DaoTrang.NoiToChuc.ToLower().Contains(noitochuc.ToLower()));
            }
            if(ngayguidon.HasValue)
            {
                query = query.Where(x => x.NgayGuiDon == ngayguidon);
            }
            if (trangthaidon.HasValue)
            {
                query = query.Where(x => x.TrangThaiDon == trangthaidon);
            }
            return query;
        }

        public IQueryable<DonDangKy> GetDonDangKyByTruTri(int nguoitrutri, string? noitochuc, DateTime? ngayguidon, int? trangthaidon)
        {
            var listdaotrang = appDbContext.DaoTrang.Include(x => x.LstDondangkys).ThenInclude(y => y.PhatTu).Where(x => x.NguoiTruTri == nguoitrutri);
            List<DonDangKy> query = new List<DonDangKy>();
            foreach(DaoTrang daoTrang in listdaotrang) 
            {
                query = query.Concat(daoTrang.LstDondangkys.ToList()).ToList();
            }
            IQueryable<DonDangKy> Iquery = query.AsQueryable();
            if (!string.IsNullOrWhiteSpace(noitochuc))
            {
                Iquery = Iquery.Where(x => x.DaoTrang.NoiToChuc.ToLower().Contains(noitochuc.ToLower()));
            }
            if (ngayguidon.HasValue)
            {
                Iquery = Iquery.Where(x => x.NgayGuiDon == ngayguidon);
            }
            if (trangthaidon.HasValue)
            {
                Iquery = Iquery.Where(x => x.TrangThaiDon == trangthaidon);
            }
            return Iquery;
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
                if (xacnhandon.TrangThaiDon == 0 && daotrang.SoThanhVienThamGia>0)
                {
                    daotrang.SoThanhVienThamGia -= 1;
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
