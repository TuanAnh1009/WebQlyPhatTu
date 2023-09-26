using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebQlyPhatTu.Dto;
using WebQlyPhatTu.Helper;
using WebQlyPhatTu.IServices;
using WebQlyPhatTu.Models;
using WebQlyPhatTu.Sevices;

namespace WebQlyPhatTu.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserServices userServices;
        private readonly IConfiguration configuration;
        private readonly IGetInfoFromToken getInfo;

        public AccountController(IConfiguration _configuration)
        {
            userServices = new UserServices();
            configuration = _configuration;
            getInfo = new GetInfoFromTokenService();
        }

        [HttpGet]
        public IActionResult Index()
        {
            string token = Request.Cookies["token"];
            if(!string.IsNullOrEmpty(token))
            {
                PhatTu user = getInfo.GetUserFromToken(token);
                return View(user);
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public IActionResult Register(Register dto)
        {
            var res = userServices.Register(dto);
            if (ModelState.IsValid)
            {
                if (!res.Error)
                {
                    return RedirectToAction("Login");
                } else
                {
                    ModelState.AddModelError("Email", res.Mes);
                }
            }
            return View("Register");
        }

        [HttpGet]
        public IActionResult Login()
        {
            string token = Request.Cookies["token"];
            if (!string.IsNullOrEmpty(token)){
                Logout();
            }
            return View("Login");
        }

        [HttpPost]
        public IActionResult Login(Login dto)
        {
            var res = userServices.Login(dto);
            if (!res.Error)
            {
                var token = GenerateToken(res.Data);
                Response.Cookies.Append("token", token, new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddMinutes(20)
                });
                return RedirectToAction("Index", "Home");
            } else 
            {
                ModelState.AddModelError("Email", res.Mes);
            }
            return View("Login");
        }

        private string GenerateToken(PhatTu phatTu)
        {
            var Token = new JwtSecurityTokenHandler();
            //var SecretKeyBytes = Encoding.UTF8.GetBytes(appsettings.SecretKey);
            var SecretKeyBytes = Encoding.UTF8.GetBytes(configuration["Appsettings:SecretKey"]);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, phatTu.Email),
                    new Claim(ClaimTypes.Name, phatTu.Ho+phatTu.TenDem+phatTu.Ten),
                    new Claim("PhatTuId", phatTu.PhatTuId.ToString()),
                    //role
                    new Claim(ClaimTypes.Role, phatTu.UserRoles.Role.Code),
                    new Claim(ClaimTypes.Role, phatTu.KieuThanhVien.Code)
                }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(SecretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
            };
            var TokenHandler = Token.CreateToken(tokenDescription);
            return Token.WriteToken(TokenHandler);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("token");
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> UploadAvatar(IFormFile file)
        {
            try
            {
                var token = Request.Cookies["token"];
                var userid = getInfo.GetIdFromToken(token);
                string link = await UploadFile.Upload(file);
                var res = await userServices.UploadAvatar(userid, link);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult DeleteAvatar()
        {
            var token = Request.Cookies["token"];
            var userid = getInfo.GetIdFromToken(token);
            var res = userServices.DeleteAvatar(userid);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateUser(UpdateUserDto dto)
        {
            var res = userServices.UpdateUser(dto);
            return RedirectToAction("Index");
        }
    }
}
