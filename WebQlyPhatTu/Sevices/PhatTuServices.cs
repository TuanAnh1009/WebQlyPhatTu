using Microsoft.EntityFrameworkCore;
using System;
using WebQlyPhatTu.Dto;
using WebQlyPhatTu.Helper;
using WebQlyPhatTu.IServices;
using WebQlyPhatTu.Models;

namespace WebQlyPhatTu.Sevices
{
    public class PhatTuServices : IPhatTuServices
    {
        private readonly AppDbContext dbContext;

        public PhatTuServices()
        {
            dbContext = new AppDbContext();
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public ReturnObject<PhatTu> AddPhatTu(AddPhatTuDto dto)
        {
            try
            {
                if (!IsValidEmail(dto.Email))
                {
                    throw new Exception("Email invalidate");
                }
                if (dbContext.PhatTu.Any(x => x.Email == dto.Email))
                {
                    throw new Exception("Email already exists");
                }
                var addPhatTu = new PhatTu
                {
                    DaHoanTuc = dto.DaHoanTuc,
                    Email = dto.Email,
                    GioiTinh = dto.GioiTinh,
                    Ho = dto.Ho,
                    NgayCapNhat = DateTime.UtcNow,
                    NgayHoanTuc = dto?.NgayHoanTuc,
                    NgaySinh = dto?.NgaySinh,
                    NgayXuatGia = dto?.NgayXuatGia,
                    PassWord = BCrypt.Net.BCrypt.HashPassword(dto.PassWord),
                    PhapDanh = dto?.PhapDanh,
                    SoDienThoai = dto?.SoDienThoai,
                    Ten = dto?.Ten,
                    TenDem = dto?.TenDem,
                    ChuaId = dto?.ChuaId,
                    KieuThanhVienId = dto?.KieuThanhVienId,
                    UserRoles = new UserRoles
                    {
                        RoleId = 2
                    }
                };
                dbContext.Add(addPhatTu);
                dbContext.SaveChanges();
                return new ReturnObject<PhatTu>
                {
                    Mes = "Create success",
                    Data = addPhatTu
                };
            }
            catch (Exception ex)
            {
                return new ReturnObject<PhatTu>
                {
                    Mes = ex.Message,
                    Error = true,
                    Data = null
                };
            }
        }

        public IQueryable<PhatTu> DSPT(string? name, string? phapdanh, string? email, int? gioitinh)
        {
           var query = dbContext.PhatTu.Include(x => x.Dondangkys).AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Ten.ToLower().Contains(name.ToLower()));
            }
            if (!string.IsNullOrEmpty(phapdanh))
            {
                query = query.Where(x => x.PhapDanh.ToLower().Contains(phapdanh.ToLower()));
            }
            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(x => x.Email.ToLower().Contains(email.ToLower()));
            }
            if (gioitinh.HasValue)
            {
                query = query.Where(x => x.GioiTinh == gioitinh);
            }
            return query;
        }

        public ReturnObject<PhatTu> EditPhatTu(EditPhatTuDto dto)
        {
            try
            {
                var updatePhatTu = dbContext.PhatTu.SingleOrDefault(x => x.PhatTuId == dto.PhatTuId);
                if (updatePhatTu == null)
                {
                    throw new Exception("Not exists");
                }
                if (dbContext.PhatTu.Any(x => x.Email == dto.Email && x.PhatTuId != dto.PhatTuId))
                {
                    throw new Exception("Email already exists");
                }

                if (dto.DaHoanTuc.HasValue)
                    updatePhatTu.DaHoanTuc = dto.DaHoanTuc;
                if (!string.IsNullOrEmpty(dto.Email))
                    updatePhatTu.Email = dto.Email;
                if (dto.GioiTinh.HasValue)
                    updatePhatTu.GioiTinh = dto.GioiTinh;
                if (!string.IsNullOrEmpty(dto.Ho))
                    updatePhatTu.Ho = dto.Ho;
                if (dto.NgayHoanTuc.HasValue)
                    updatePhatTu.NgayHoanTuc = dto.NgayHoanTuc;
                if (dto.NgaySinh.HasValue)
                    updatePhatTu.NgaySinh = dto.NgaySinh;
                if (dto.NgayXuatGia.HasValue)
                    updatePhatTu.NgayXuatGia = dto?.NgayXuatGia;
                if (!string.IsNullOrEmpty(dto.PassWord))
                    updatePhatTu.PassWord = BCrypt.Net.BCrypt.HashPassword(dto.PassWord);
                if (!string.IsNullOrEmpty(dto.PhapDanh))
                    updatePhatTu.PhapDanh = dto.PhapDanh;
                if (!string.IsNullOrEmpty(dto.SoDienThoai))
                    updatePhatTu.SoDienThoai = dto.SoDienThoai;
                if (!string.IsNullOrEmpty(dto.Ten))
                    updatePhatTu.Ten = dto?.Ten;
                if (!string.IsNullOrEmpty(dto.TenDem))
                    updatePhatTu.TenDem = dto?.TenDem;
                if (dto.ChuaId.HasValue)
                    updatePhatTu.ChuaId = dto?.ChuaId;
                if (dto.KieuThanhVienId.HasValue)
                    updatePhatTu.KieuThanhVienId = dto?.KieuThanhVienId;
                updatePhatTu.NgayCapNhat = DateTime.UtcNow;
                dbContext.Update(updatePhatTu);
                dbContext.SaveChanges();
                return new ReturnObject<PhatTu>
                {
                    Mes = "Create success",
                    Data = updatePhatTu
                };
            }
            catch (Exception ex)
            {
                return new ReturnObject<PhatTu>
                {
                    Mes = ex.Message,
                    Error = true,
                    Data = null
                };
            }
        }

        public List<ObjList> ListChua()
        {
            var listchua = dbContext.Chua.ToList();
            List<ObjList> objlist = new List<ObjList>();
            foreach(var item in listchua)
            {
                objlist.Add(new ObjList
                {
                    Id = item.ChuaId,
                    Name = item.TenChua
                });
            }
            return objlist;
        }

        public List<string> ListKieuThanhVien()
        {
            throw new NotImplementedException();
        }
    }
}
