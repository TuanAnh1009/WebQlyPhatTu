using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WebQlyPhatTu.Dto;
using WebQlyPhatTu.IServices;
using WebQlyPhatTu.Sevices;
using WebQlyPhatTu.Helper;
using WebQlyPhatTu.Models;

namespace WebQlyPhatTu.Controllers
{
    public class DonDangKyController : Controller
    {
        private readonly IDonDangKyServices donDangKyServices;
        private readonly IGetInfoFromToken getInfo;

        public DonDangKyController()
        {
            donDangKyServices = new DonDangKyServices();
            getInfo = new GetInfoFromTokenService();
        }

        [HttpPost]
        public IActionResult XacNhanDon(XacNhanDonDto dto)
        {
            string token = Request.Cookies["token"];
            if (!string.IsNullOrEmpty(token))
            {
                int userid = getInfo.GetIdFromToken(token);
                var res = donDangKyServices.XacNhanDon(userid, dto);
            }
            return RedirectToAction("Index", "QuanLyDonDangKy");
        }

        [HttpPost]
        public IActionResult GuiDon(DonDangKyDto dto)
        {
            string token = Request.Cookies["token"];
            if (!string.IsNullOrEmpty(token)) 
            {
                int userid = getInfo.GetIdFromToken(token);
                var res = donDangKyServices.GuiDon(userid, dto);
                return RedirectToAction("Index", "DaoTrang");
            } else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
    }
}
