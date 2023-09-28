using Microsoft.AspNetCore.Mvc;
using WebQlyPhatTu.Helper;
using WebQlyPhatTu.IServices;
using WebQlyPhatTu.Models;
using WebQlyPhatTu.Sevices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebQlyPhatTu.Controllers
{
    public class QuanLyDonDangKyController : Controller
    {
        private readonly IDonDangKyServices donDangKyServices;
        private readonly IGetInfoFromToken getInfo;

        public QuanLyDonDangKyController()
        {
            donDangKyServices = new DonDangKyServices();
            getInfo = new GetInfoFromTokenService();
        }

        [HttpGet]
        public IActionResult Index(string? noitochuc, DateTime? ngayguidon, int? trangthaidon, Pagination? pagination)
        {
            string token = Request.Cookies["token"];
            
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }
            var user = getInfo.GetUserFromToken(token);
            if (token != null && user.UserRoles.Role.Code == "ADMIN" || user.KieuThanhVien.Code == "TRUTRI")
            {
                IQueryable<DonDangKy> query;
                if (user.UserRoles.Role.Code == "ADMIN")
                {
                    query = donDangKyServices.GetDonDangKy(noitochuc, ngayguidon, trangthaidon);
                }
                else
                {
                    query = donDangKyServices.GetDonDangKyByTruTri(user.PhatTuId, noitochuc, ngayguidon, trangthaidon);
                }
                var listdondangky = PageResult<DonDangKy>.ToPageResult(pagination!, query);
                pagination!.TotalCount = query.ToList().Count();
                PageResult<DonDangKy> pagexacnhandon = new PageResult<DonDangKy>(pagination, listdondangky);
                return View(pagexacnhandon);
            } else
            {
                return BadRequest("Không thể truy cập");
            } 
            
        }
    }
}
