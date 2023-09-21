using Microsoft.AspNetCore.Mvc;
using WebQlyPhatTu.Dto;
using WebQlyPhatTu.Helper;
using WebQlyPhatTu.IServices;
using WebQlyPhatTu.Models;
using WebQlyPhatTu.Sevices;

namespace WebQlyPhatTu.Controllers
{
    public class QuanLyChuaController : Controller
    {
        private readonly IChuaServices chuaServices;
        private readonly IGetInfoFromToken getInfo;

        public QuanLyChuaController()
        {
            chuaServices = new ChuaServices();
            getInfo = new GetInfoFromTokenService();
        }

        [HttpGet]
        public IActionResult Index(string? tenchua, Pagination? pagination)
        {
            string token = Request.Cookies["token"];
            var user = getInfo.GetUserFromToken(token);
            if (token != null && user.UserRoles.Role.Code == "ADMIN")
            {
                var query = chuaServices.GetChua(tenchua);
                var listchua = PageResult<Chua>.ToPageResult(pagination!, query);
                pagination!.TotalCount = query.ToList().Count();
                var pagechua = new PageResult<Chua>(pagination, listchua);
                return View(pagechua);
            }
            else
            {
                return BadRequest("Không thể truy cập");
            }
        }

        [HttpPost]
        public IActionResult Create(ChuaDto dto)
        {
            var res = chuaServices.AddChua(dto);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(ChuaDto dto)
        {
            var res = chuaServices.UpdateChua(dto);
            return RedirectToAction("Index");
        }
    }
}
