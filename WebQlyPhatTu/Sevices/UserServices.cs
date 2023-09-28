using Microsoft.EntityFrameworkCore;
using System;
using WebQlyPhatTu.Dto;
using WebQlyPhatTu.Helper;
using WebQlyPhatTu.IServices;
using WebQlyPhatTu.Models;

namespace WebQlyPhatTu.Sevices
{
    public class UserServices : IUserServices

    {
        private readonly AppDbContext dbContext;

        public UserServices()
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

        public string DeleteAvatar(int id)
        {
            try
            {
                var user = dbContext.PhatTu.SingleOrDefault(x => x.PhatTuId == id);
                user.AnhChup = null;
                dbContext.Update(user);
                dbContext.SaveChanges();
                return "Xóa thành công";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public ReturnObject<PhatTu> GetbyId(int phatTuId)
        {
            try
            {
                var phatTu = dbContext.PhatTu.SingleOrDefault(x => x.PhatTuId == phatTuId);
                if(phatTu != null)
                {
                    throw new Exception("Account not exists");
                }
                return new ReturnObject<PhatTu>
                {
                    Error = false,
                    Mes = "Success",
                    Data = phatTu!
                };
            }
            catch (Exception ex)
            {
                return new ReturnObject<PhatTu>
                {
                    Error = true,
                    Mes = ex.Message,
                    Data = null
                };
            }
        }
        public string GetName(int id)
        {
            var user = dbContext.PhatTu.SingleOrDefault(x => x.PhatTuId == id);
            return user.Ho + " " + user.TenDem + " " + user.Ten;
        }

        public ReturnObject<PhatTu> Login(Login dto)
        {
            var User = dbContext.PhatTu.Include(z => z.KieuThanhVien).Include(x => x.UserRoles).ThenInclude(y => y.Role).SingleOrDefault(u => u.Email == dto.Email);
            try
            {
                if (string.IsNullOrEmpty(dto.PassWord) || string.IsNullOrEmpty(dto.Email))
                {
                    throw new Exception("Email hoặc mật khẩu không được để trống.");
                }
                if (User == null || BCrypt.Net.BCrypt.Verify(dto.PassWord, User.PassWord) == false)
                {
                    throw new Exception("Email hoặc mật khẩu không chính sác.");
                }
                if (User.IsActive == false)
                {
                    throw new Exception("Tài khoản đã bị khóa.");
                }
            }
            catch (Exception ex)
            {
                return new ReturnObject<PhatTu>
                {
                    Error = true,
                    Mes = ex.Message,
                    Data = null
                };
            }
            return new ReturnObject<PhatTu>
            {
                Mes = "Loggin success",
                Data = User
            };
        }

        public ReturnObject<PhatTu> Register(Register dto)
        {
            try
            {
                if (dbContext.PhatTu.Any(x => x.Email == dto.Email))
                {
                    throw new Exception("Email đã được sử dụng.");
                }
                if (!IsValidEmail(dto.Email))
                {
                    throw new Exception("Email không đúng định dạng.");
                }
            }
            catch (Exception ex)
            {
                return new ReturnObject<PhatTu>
                {
                    Error = true,
                    Mes = ex.Message,
                    Data = null
                };
            }
            PhatTu CreatePhatTu = new PhatTu()
            {
                Ho = dto.Ho,
                TenDem = dto.TenDem,
                Ten = dto.Ten,
                NgaySinh = dto.NgaySinh,
                GioiTinh = dto.GioiTinh,
                Email = dto.Email,
                PassWord = BCrypt.Net.BCrypt.HashPassword(dto.PassWord),
                UserRoles = new UserRoles()
                {
                    RoleId = 2
                },
                KieuThanhVienId = 2
            };

            dbContext.Add(CreatePhatTu);
            dbContext.SaveChanges();
            return new ReturnObject<PhatTu>
            {
                Mes = "Sign Up Success",
                Data = CreatePhatTu
            };
        }

        public ReturnObject<UpdateUserDto> UpdateUser(UpdateUserDto dto)
        {
            try
            {
                var user = dbContext.PhatTu.SingleOrDefault(x => x.PhatTuId == dto.PhatTuId);
                if(user == null)
                {
                    throw new Exception("Tài khoản không tồn tại");
                };
                user.Email = dto.Email;
                user.GioiTinh = dto.GioiTinh;
                user.Ho = dto.Ho;
                user.NgaySinh = dto.NgaySinh;
                user.PhapDanh = dto.PhapDanh;
                user.SoDienThoai = dto.SoDienThoai;
                user.Ten = dto.Ten;
                user.TenDem = dto.TenDem;
                user.ChuaId = dto.ChuaId;
                dbContext.Update(user);
                dbContext.SaveChanges();
                return new ReturnObject<UpdateUserDto>
                {
                    Error = false,
                    Mes = "Cập nhật thành công",
                    Data = dto
                };
            } catch (Exception ex)
            {
                return new ReturnObject<UpdateUserDto>
                {
                    Error = true,
                    Mes = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<string> UploadAvatar(int id, string avatarUrl)
        {
            var user = dbContext.PhatTu.SingleOrDefault(x => x.PhatTuId == id);
            if (user == null)
            {
                throw new Exception("Not logged in");
            }
            user.AnhChup = avatarUrl;
            dbContext.Update(user);
            dbContext.SaveChanges();
            return "success";
        }
    }
}
