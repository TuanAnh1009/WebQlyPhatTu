using WebQlyPhatTu.Dto;
using WebQlyPhatTu.Helper;
using WebQlyPhatTu.IServices;
using WebQlyPhatTu.Models;

namespace WebQlyPhatTu.Sevices
{
    public class DaoTrangServices : IDaoTrangServices
    {
        private readonly AppDbContext appDbContext;

        public DaoTrangServices()
        {
            appDbContext = new AppDbContext();
        }

        public ReturnObject<DaoTrang> AddDaoTrang(DaoTrangDto dto)
        {
            try
            {
                var addDaoTrang = new DaoTrang
                {
                    NoiDung = dto.NoiDung,
                    NoiToChuc = dto.NoiToChuc,
                    NguoiTruTri = dto.NguoiTruTri
                };
                appDbContext.Add(addDaoTrang);
                appDbContext.SaveChanges();
                return new ReturnObject<DaoTrang>
                {
                    Mes = "Success",
                    Data = addDaoTrang
                };
            }
            catch (Exception ex)
            {
                return new ReturnObject<DaoTrang>
                {
                    Mes = ex.Message,
                    Data = null,
                    Error = true
                };
            }
        }

        public ReturnObject<string> DeleteDaoTrang(int id)
        {
            try
            {
                var deleteDaoTrang = appDbContext.DaoTrang.SingleOrDefault(x => x.DaoTrangId == id);
                if (deleteDaoTrang == null)
                {
                    throw new Exception("Not exist");
                }
                appDbContext.Remove(deleteDaoTrang);
                appDbContext.SaveChanges();
                return new ReturnObject<string>
                {
                    Mes = "Delete success",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ReturnObject<string>
                {
                    Mes = ex.Message,
                    Data = null
                };
            }
        }

        public IQueryable<DaoTrang> GetDaoTrang(string? noitochuc)
        {
            var query = appDbContext.DaoTrang.AsQueryable();
            if (!string.IsNullOrEmpty(noitochuc))
            {
                query = query.Where(x => x.NoiToChuc.ToLower().Contains(noitochuc.ToLower()));
            }
            return query;
        }

        public IQueryable<DaoTrang> GetDaoTrangByTruTri(int trutriid,string? noitochuc)
        {
            var query = appDbContext.DaoTrang.Where(x => x.NguoiTruTri == trutriid).AsQueryable();
            if (!string.IsNullOrEmpty(noitochuc))
            {
                query = query.Where(x => x.NoiToChuc.ToLower().Contains(noitochuc.ToLower()));
            }
            return query;
        }

        public IQueryable<DaoTrang> GetDaoTrangActive(string? noitochuc)
        {
            var query = appDbContext.DaoTrang.Where(x => x.DaKetThuc == false).AsQueryable();
            if (!string.IsNullOrEmpty(noitochuc))
            {
                query = query.Where(x => x.NoiToChuc.ToLower().Contains(noitochuc.ToLower()));
            }
            return query;
        }

        public string GetNameTruTri(int id)
        {
            var trutri = appDbContext.PhatTu.SingleOrDefault(x => x.PhatTuId == id);
            return trutri.Ho + " " + trutri.TenDem + " " + trutri.Ten;
        }

        public ReturnObject<DaoTrang> UpdateDaoTrang(DaoTrangDto dto)
        {
            try
            {
                var updateDaoTrang = appDbContext.DaoTrang.SingleOrDefault(x => x.DaoTrangId == dto.DaoTrangId);
                if (updateDaoTrang == null)
                {
                    throw new Exception("Not exist");
                }
                updateDaoTrang.NoiDung = dto.NoiDung;
                updateDaoTrang.NoiToChuc = dto.NoiToChuc;
                updateDaoTrang.DaKetThuc = dto.DaKetThuc;
                updateDaoTrang.NguoiTruTri = dto.NguoiTruTri;
                appDbContext.Update(updateDaoTrang);
                appDbContext.SaveChanges();
                return new ReturnObject<DaoTrang>
                {
                    Mes = "update success",
                    Data = updateDaoTrang
                };
            }
            catch (Exception ex)
            {
                return new ReturnObject<DaoTrang>
                {
                    Mes = ex.Message,
                    Data = null
                };
            }
        }
    }
}
