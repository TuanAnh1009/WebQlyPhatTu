using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebQlyPhatTu.Dto;
using WebQlyPhatTu.IServices;
using WebQlyPhatTu.Models;
using WebQlyPhatTu.Sevices;

namespace WebQlyPhatTu.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserServices userServices;
        private readonly IConfiguration configuration;

        public AccountController(IConfiguration _configuration)
        {
            userServices = new UserServices();
            configuration = _configuration;
        }

        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public IActionResult Register(Register dto)
        {
            var res = userServices.Register(dto);
            return RedirectToAction("Index", "PhatTu");
        }

        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public IActionResult Login(Login dto)
        {
            var res = userServices.Login(dto);
            if(!res.Error)
            {
                var token = GenerateToken(res.Data);
                Response.Cookies.Append("token", token, new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddMinutes(20)
                });
            }
            return RedirectToAction("Index", "PhatTu");
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
        public static string GetEmailFromToken(string jwtToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(jwtToken);

            var emailClaim = token.Claims.FirstOrDefault(c => c.Type == "email");
            if (emailClaim != null)
            {
                return emailClaim.Value;
            }
            return null;
        }
    }
}
