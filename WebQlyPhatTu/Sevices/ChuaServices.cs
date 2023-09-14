using WebQlyPhatTu.Dto;
using WebQlyPhatTu.Helper;
using WebQlyPhatTu.IServices;
using WebQlyPhatTu.Models;

namespace WebQlyPhatTu.Sevices
{
    public class ChuaServices : IChuaServices
    {
        private readonly AppDbContext appDbContext;

        public ChuaServices()
        {
            appDbContext = new AppDbContext();
        }

        public ReturnObject<Chua> AddChua(ChuaDto dto)
        {
            try
            {
                var addChua = new Chua
                {
                    CapNhap = DateTime.UtcNow,
                    DiaChi = dto.DiaChi,
                    NgayThanhLap = dto.NgayThanhLap,
                    TenChua = dto.TenChua,
                    TruTri = dto.TruTri,

                };
                appDbContext.Add(addChua);
                appDbContext.SaveChanges();
                return new ReturnObject<Chua>
                {
                    Mes = "Success",
                    Data = addChua
                };
            }
            catch
            {
                return new ReturnObject<Chua>
                {
                    Mes = "Failed",
                    Data = null,
                    Error = true
                };
            }
        }

        public ReturnObject<string> DeleteChua(int chuaId)
        {
            try
            {
                var deleteChua = appDbContext.Chua.SingleOrDefault(x => x.ChuaId == chuaId);
                if (deleteChua == null)
                {
                    throw new Exception("Not exist");
                }
                appDbContext.Remove(deleteChua);
                appDbContext.SaveChanges();
                return new ReturnObject<string>
                {
                    Mes = "Success",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ReturnObject<string>
                {
                    Mes = ex.Message,
                    Data = null,
                    Error = true
                };
            }
        }

        public IQueryable<Chua> GetChua(string? tenChua)
        {
            var query = appDbContext.Chua.AsQueryable();
            if (!string.IsNullOrEmpty(tenChua))
            {
                query = query.Where(x => x.TenChua.ToLower().Contains(tenChua.ToLower()));
            }
            return query;
        }

        public ReturnObject<Chua> UpdateChua(ChuaDto dto)
        {
            try
            {
                var updateChua = appDbContext.Chua.SingleOrDefault(x => x.ChuaId == dto.ChuaId);
                if (updateChua == null)
                {
                    throw new Exception("Not exist");
                }
                updateChua.DiaChi = dto.DiaChi;
                updateChua.NgayThanhLap = dto.NgayThanhLap;
                updateChua.TenChua = dto.TenChua;
                updateChua.TruTri = dto.TruTri;
                updateChua.CapNhap = DateTime.UtcNow;
                appDbContext.Update(updateChua);
                appDbContext.SaveChanges();
                return new ReturnObject<Chua>
                {
                    Mes = "Success",
                    Data = updateChua
                };
            }
            catch (Exception ex)
            {
                return new ReturnObject<Chua>
                {
                    Mes = ex.Message,
                    Data = null,
                    Error = true
                };
            }
        }
    }
}
