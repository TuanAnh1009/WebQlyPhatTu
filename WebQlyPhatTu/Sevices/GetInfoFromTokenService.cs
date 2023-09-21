using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using WebQlyPhatTu.IServices;
using WebQlyPhatTu.Models;

namespace WebQlyPhatTu.Sevices
{
    public class GetInfoFromTokenService : IGetInfoFromToken
    {
        private readonly AppDbContext dbContext;

        public GetInfoFromTokenService()
        {
            dbContext = new AppDbContext();
        }

        public string GetEmailFromToken(string token)
        {
            throw new NotImplementedException();
        }

        public int GetIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            var phatTuId = jwtToken.Claims.FirstOrDefault(c => c.Type == "PhatTuId");
            if (int.TryParse(phatTuId.Value, out int id))
            {
                return id;
            }
            else
            {
                return -1;
            }
        }

        public PhatTu GetUserFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            var phatTuId = jwtToken.Claims.FirstOrDefault(c => c.Type == "PhatTuId");
            if(int.TryParse(phatTuId.Value, out int id))
            {
                var phatTu = dbContext.PhatTu.Include(a => a.KieuThanhVien).Include(x => x.UserRoles).ThenInclude(y => y.Role).SingleOrDefault(x => x.PhatTuId == id);
                return phatTu;
            }
            else
            {
                return null;
            }
            
        }
    }
}
