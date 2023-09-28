using Microsoft.AspNetCore.Mvc;
using WebQlyPhatTu.Dto;
using WebQlyPhatTu.IServices;
using WebQlyPhatTu.Sevices;

namespace WebQlyPhatTu.Controllers
{
    public class PasswordController : Controller
    {
        private readonly IPasswordServices passwordServices;

        public PasswordController()
        {
            passwordServices = new PasswordServices();
        }

        public IActionResult ChangePassword(string email,ChangePassword dto)
        {
            var res = passwordServices.ChangePassword(email, dto);
            if (ModelState.IsValid)
            {
                if (!res.Error)
                {
                    return RedirectToAction("Index", "Account");
                }
                else
                {
                    if(res.Mes == "Nhập lại mật khẩu không chính xác.")
                    {
                        ModelState.AddModelError("ReNewPassword", res.Mes);
                    }
                    if(res.Mes == "Mật khẩu không đúng.")
                    {
                        ModelState.AddModelError("OldPassword", res.Mes);
                    }
                    
                }
            }
            return View("Register");
        }
    }
}
