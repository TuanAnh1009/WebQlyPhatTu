using WebQlyPhatTu.Dto;
using WebQlyPhatTu.Helper;
using WebQlyPhatTu.Models;

namespace WebQlyPhatTu.IServices
{
    public interface IUserServices
    {
        ReturnObject<PhatTu> Register(Register dto);
        ReturnObject<PhatTu> Login(Login dto);
        ReturnObject<PhatTu> GetbyId(int phatTuId);
    }
}
