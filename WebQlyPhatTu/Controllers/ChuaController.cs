using Microsoft.AspNetCore.Mvc;
using WebQlyPhatTu.Dto;
using WebQlyPhatTu.Helper;
using WebQlyPhatTu.IServices;
using WebQlyPhatTu.Models;
using WebQlyPhatTu.Sevices;

namespace WebQlyPhatTu.Controllers
{
    public class ChuaController : Controller
    {
        private readonly IChuaServices chuaServices;

        public ChuaController()
        {
            chuaServices = new ChuaServices();
        }

        [HttpGet]
        public IActionResult Index(string? tenchua, Pagination? pagination)
        {
            var query = chuaServices.GetChua(tenchua);
            var listchua = PageResult<Chua>.ToPageResult(pagination!, query);
            pagination!.TotalCount = query.ToList().Count();
            var pagechua = new PageResult<Chua>(pagination, listchua);
            return View(pagechua);
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
