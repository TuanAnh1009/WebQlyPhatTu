using WebQlyPhatTu.Models;

namespace WebQlyPhatTu.IServices
{
    public interface IGetInfoFromToken
    {
        PhatTu GetUserFromToken(string token);
        string GetEmailFromToken(string token);
        int GetIdFromToken(string token);
    }
}
