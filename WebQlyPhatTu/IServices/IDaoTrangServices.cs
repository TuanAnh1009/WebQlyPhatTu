using WebQlyPhatTu.Dto;
using WebQlyPhatTu.Helper;
using WebQlyPhatTu.Models;

namespace WebQlyPhatTu.IServices
{
    public interface IDaoTrangServices
    {
        ReturnObject<DaoTrang> AddDaoTrang(DaoTrangDto dto);
        ReturnObject<DaoTrang> UpdateDaoTrang(DaoTrangDto dto);
        ReturnObject<string> DeleteDaoTrang(int id);
        IQueryable<DaoTrang> GetDaoTrang(string? noitochuc);
        IQueryable<DaoTrang> GetDaoTrangByTruTri(int trutriid,string? noitochuc);
        IQueryable<DaoTrang> GetDaoTrangActive(string? noitochuc);
    }
}
