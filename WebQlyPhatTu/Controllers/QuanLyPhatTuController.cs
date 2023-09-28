using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebQlyPhatTu.Dto;
using WebQlyPhatTu.Helper;
using WebQlyPhatTu.IServices;
using WebQlyPhatTu.Models;
using WebQlyPhatTu.Sevices;

namespace WebQlyPhatTu.Controllers
{
    public class QuanLyPhatTuController : Controller
    {
        private readonly IPhatTuServices phatTuServices;
        private readonly IGetInfoFromToken getInfo;

        public QuanLyPhatTuController()
        {
            phatTuServices = new PhatTuServices();
            getInfo = new GetInfoFromTokenService();
        }

        [HttpGet]
        public IActionResult Index(string? name, string? phapdanh, string? email, int? gioitinh, Pagination? pagination)
        {
            string token = Request.Cookies["token"];
            
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }
            var user = getInfo.GetUserFromToken(token);
            if (token != null && user.UserRoles.Role.Code == "ADMIN")
            {
                var query = phatTuServices.DSPT(name, phapdanh, email, gioitinh);
                var listpt = PageResult<PhatTu>.ToPageResult(pagination, query);
                pagination.TotalCount = query.ToList().Count();
                var pagept = new PageResult<PhatTu>(pagination, listpt);
                var listchua = phatTuServices.ListChua();
                var listkieuthanhvien = phatTuServices.ListKieuThanhVien();
                var objpt = new ObjPhatTu
                {
                    ListPhatTu = pagept,
                    ListChua = listchua,
                    ListKieuThanhVien = listkieuthanhvien,
                };
                return View(objpt);
            } else
            {
                return BadRequest("Không thể truy cập");
            }
        }

        [HttpPost]
        public IActionResult Create(AddPhatTuDto dto)
        {
            var res = phatTuServices.AddPhatTu(dto);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(EditPhatTuDto dto)
        {
            var res = phatTuServices.EditPhatTu(dto);
            return RedirectToAction("Index");
        }
    }
}
