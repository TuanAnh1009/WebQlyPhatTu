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

        public ReturnObject<PhatTu> Login(Login dto)
        {
            var User = dbContext.PhatTu.Include(z => z.KieuThanhVien).Include(x => x.UserRoles).ThenInclude(y => y.Role).SingleOrDefault(u => u.Email == dto.Email);
            try
            {
                if (string.IsNullOrEmpty(dto.PassWord) || string.IsNullOrEmpty(dto.Email))
                {
                    throw new Exception("Email or password can't be blank");
                }
                if (User == null || BCrypt.Net.BCrypt.Verify(dto.PassWord, User.PassWord) == false)
                {
                    throw new Exception("email or password is incorrect");
                }
                if (User.IsActive == false)
                {
                    throw new Exception("Account has been deleted");
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
                    throw new Exception("Email already exists");
                }
                if (!IsValidEmail(dto.Email))
                {
                    throw new Exception("Email invalidate");
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
    }
}
