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
        Task<string> UploadAvatar(int id, string avatarUrl);
        ReturnObject<UpdateUserDto> UpdateUser(UpdateUserDto dto);
        string DeleteAvatar(int id);
        string GetName(int id);
    }
}
