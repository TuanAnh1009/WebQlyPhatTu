using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebQlyPhatTu.Dto;
using WebQlyPhatTu.Helper;
using WebQlyPhatTu.IServices;
using WebQlyPhatTu.Models;
using WebQlyPhatTu.Sevices;

namespace WebQlyPhatTu.Controllers
{
    public class PhatTuController : Controller
    {
        private readonly IPhatTuServices phatTuServices;

        public PhatTuController()
        {
            phatTuServices = new PhatTuServices();
        }

        [HttpGet]
        public IActionResult Index(string? name, string? phapdanh, string? email, int? gioitinh, Pagination? pagination)
        {
            var query = phatTuServices.DSPT(name,phapdanh,email,gioitinh);
            var listpt = PageResult<PhatTu>.ToPageResult(pagination, query);
            pagination.TotalCount = query.ToList().Count();
            var pagept = new PageResult<PhatTu>(pagination, listpt);
            var listchua = phatTuServices.ListChua();
            var objpt = new ObjPhatTu
            {
                ListPhatTu = pagept,
                ListChua = listchua
            };
            return View(objpt);
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
