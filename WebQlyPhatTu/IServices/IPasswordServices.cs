using WebQlyPhatTu.Dto;
using WebQlyPhatTu.Helper;
using WebQlyPhatTu.Models;

namespace WebQlyPhatTu.IServices
{
    public interface IPasswordServices
    {
        PhatTu ForgetPassword(string email);
        PasswordRefresh CreateRefreshPassword(PhatTu phatTu);
        ReturnObject<string> RefreshWithPassword(RefreshwithCode dto);
        ReturnObject<string> ChangePassword(string email, ChangePassword dto);
    }
}
