using Microsoft.AspNetCore.Mvc;
using WebQlyPhatTu.Helper;
using WebQlyPhatTu.IServices;
using WebQlyPhatTu.Models;
using WebQlyPhatTu.Sevices;

namespace WebQlyPhatTu.Controllers
{
    public class DaoTrangController : Controller
    {
        private readonly IDaoTrangServices daoTrangServices;
        private readonly IGetInfoFromToken getInfo;
        private readonly AppDbContext appDbContext;

        public DaoTrangController()
        {
            daoTrangServices = new DaoTrangServices();
            getInfo = new GetInfoFromTokenService();
            appDbContext = new AppDbContext();
        }

        [HttpGet]
        public IActionResult Index(string? noitochuc, Pagination? pagination)
        {
            var query = daoTrangServices.GetDaoTrangActive(noitochuc);
            var listdaotrang = PageResult<DaoTrang>.ToPageResult(pagination!, query);
            pagination!.TotalCount = query.ToList().Count();
            var pagedaotrang = new PageResult<DaoTrang>(pagination, listdaotrang);
            return View(pagedaotrang);
        }
    }
}
