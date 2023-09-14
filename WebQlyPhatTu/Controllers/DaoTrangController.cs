using Microsoft.AspNetCore.Mvc;
using System;
using WebQlyPhatTu.Dto;
using WebQlyPhatTu.Helper;
using WebQlyPhatTu.IServices;
using WebQlyPhatTu.Models;
using WebQlyPhatTu.Sevices;

namespace WebQlyPhatTu.Controllers
{
    public class DaoTrangController : Controller
    {
        private readonly IDaoTrangServices daoTrangServices;

        public DaoTrangController()
        {
            daoTrangServices = new DaoTrangServices();
        }

        [HttpGet]
        public IActionResult Index(string? noitochuc, Pagination? pagination)
        {
            var query = daoTrangServices.GetDaoTrang(noitochuc);
            var listdaotrang = PageResult<DaoTrang>.ToPageResult(pagination!, query);
            pagination!.TotalCount = query.ToList().Count();
            var pagedaotrang = new PageResult<DaoTrang>(pagination, listdaotrang);
            return View(pagedaotrang);
        }

        [HttpPost]
        public IActionResult Create(DaoTrangDto dto)
        {
            //var token = Request.Cookies["token"];
            //var userId = GetIdFromToken(token);
            //var user = appDbContext.PhatTu.Include(x => x.UserRoles).ThenInclude(y => y.Role).FirstOrDefault(z => z.PhatTuId == userId);
            //if (user.UserRoles.Role.Code != "ADMIN")
            //{
            //    dto.NguoiTruTri = userId;
            //}
            var res = daoTrangServices.AddDaoTrang(dto);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(DaoTrangDto dto)
        {
            var res = daoTrangServices.UpdateDaoTrang(dto);
            return RedirectToAction("Index");
        }
    }
}
