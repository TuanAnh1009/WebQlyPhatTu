using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebQlyPhatTu.IServices;
using WebQlyPhatTu.Models;
using WebQlyPhatTu.Sevices;

namespace WebQlyPhatTu.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPhatTuServices phatTuServices;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            phatTuServices = new PhatTuServices();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}