using WebQlyPhatTu.Dto;
using WebQlyPhatTu.Helper;
using WebQlyPhatTu.Models;

namespace WebQlyPhatTu.IServices
{
    public interface IChuaServices
    {
        ReturnObject<Chua> AddChua(ChuaDto dto);
        ReturnObject<Chua> UpdateChua(ChuaDto dto);
        ReturnObject<string> DeleteChua(int chuaId);
        IQueryable<Chua> GetChua(string? tenChua);
    }
}
