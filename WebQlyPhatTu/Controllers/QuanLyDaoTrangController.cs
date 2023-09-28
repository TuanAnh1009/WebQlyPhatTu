using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WebQlyPhatTu.Dto;
using WebQlyPhatTu.Helper;
using WebQlyPhatTu.IServices;
using WebQlyPhatTu.Models;
using WebQlyPhatTu.Sevices;

namespace WebQlyPhatTu.Controllers
{
    public class QuanLyDaoTrangController : Controller
    {
        private readonly IDaoTrangServices daoTrangServices;
        private readonly IGetInfoFromToken getInfo;
        private readonly AppDbContext appDbContext;

        public QuanLyDaoTrangController()
        {
            daoTrangServices = new DaoTrangServices();
            getInfo = new GetInfoFromTokenService();
            appDbContext = new AppDbContext();
        }

        [HttpGet]
        public IActionResult Index(string? noitochuc, Pagination? pagination)
        {
            var token = Request.Cookies["token"];
            
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Account");
            }
            var user = getInfo.GetUserFromToken(token);
            if (token != null && user.UserRoles.Role.Code == "ADMIN" || user.KieuThanhVien.Code == "TRUTRI")
            {
                IQueryable<DaoTrang> query;
                if (user.UserRoles.Role.Code == "ADMIN")
                {
                    query = daoTrangServices.GetDaoTrang(noitochuc);
                }
                else
                {
                    query = daoTrangServices.GetDaoTrangByTruTri(user.PhatTuId, noitochuc);
                }

                IQueryable<DaoTrang> listdaotrang = PageResult<DaoTrang>.ToPageResult(pagination!, query);
                pagination!.TotalCount = query.ToList().Count();
                var pagedaotrang = new PageResult<DaoTrang>(pagination, listdaotrang);
                return View(pagedaotrang);
            } else
            {
                return BadRequest("Không thể truy cập");
            }      
        }

        [HttpPost]
        public IActionResult Create(DaoTrangDto dto)
        {
            var token = Request.Cookies["token"];
            var user = getInfo.GetUserFromToken(token);
            if (user.KieuThanhVien.Code == "TRUTRI")
            {
                dto.NguoiTruTri = user.PhatTuId;
            } 

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
