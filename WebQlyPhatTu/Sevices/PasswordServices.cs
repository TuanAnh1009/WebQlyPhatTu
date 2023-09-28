using WebQlyPhatTu.Dto;
using WebQlyPhatTu.Helper;
using WebQlyPhatTu.IServices;
using WebQlyPhatTu.Models;

namespace WebQlyPhatTu.Sevices
{
    public class PasswordServices : IPasswordServices
    {
        private readonly AppDbContext appDbContext;

        public PasswordServices()
        {
            appDbContext = new AppDbContext();
        }

        public PhatTu ForgetPassword(string email)
        {
            return appDbContext.PhatTu.SingleOrDefault(x => x.Email == email);
        }

        public PasswordRefresh CreateRefreshPassword(PhatTu phatTu)
        {
            var RefreshPassword = new PasswordRefresh
            {
                Email = phatTu.Email,
                PhatTuId = phatTu.PhatTuId,
            };
            appDbContext.Add(RefreshPassword);
            appDbContext.SaveChanges();
            return RefreshPassword;
        }

        public ReturnObject<string> RefreshWithPassword(RefreshwithCode dto)
        {
            try
            {
                var passwordRefresh = appDbContext.PasswordRefresh.SingleOrDefault(x => x.RefreshCode == dto.RefreshCode);
                //var passwordRefresh = appDbContext.PasswordRefresh.SingleOrDefault(x => x.Email == dto.Email && !x.Expired);
                if (passwordRefresh == null || passwordRefresh.ExpireAt < DateTime.UtcNow)
                {
                    throw new Exception("Refreshcode expired");
                }
                if (dto.NewPassword != dto.ReturnNewPassword)
                {
                    throw new Exception("Re-entered password is incorrect");
                }
                var User = appDbContext.PhatTu.SingleOrDefault(x => x.Email == passwordRefresh.Email);
                User.PassWord = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
                passwordRefresh.Expired = true;
                appDbContext.Update(passwordRefresh);
                appDbContext.Update(User);
                appDbContext.SaveChanges();
                return new ReturnObject<string>
                {
                    Mes = "Change password success",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ReturnObject<string>
                {
                    Mes = ex.Message,
                    Error = true,
                    Data = null
                };
            }
        }

        public ReturnObject<string> ChangePassword(string email, ChangePassword dto)
        {
            try
            {
                var User = appDbContext.PhatTu.SingleOrDefault(x => x.Email == email);
                if (User == null)
                {
                    throw new Exception("Faild");
                }
                if (!BCrypt.Net.BCrypt.Verify(dto.OldPassword, User.PassWord))
                {
                    throw new Exception("Mật khẩu không đúng.");
                }
                if (dto.NewPassword != dto.ReNewPassword)
                {
                    throw new Exception("Nhập lại mật khẩu không chính xác.");
                }
                User.PassWord = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
                User.NgayCapNhat = DateTime.UtcNow;
                appDbContext.Update(User);
                appDbContext.SaveChanges();
                return new ReturnObject<string>
                {
                    Mes = "Change success",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ReturnObject<string>
                {
                    Mes = ex.Message,
                    Error = true,
                    Data = null
                };
            }
        }
    }
}
