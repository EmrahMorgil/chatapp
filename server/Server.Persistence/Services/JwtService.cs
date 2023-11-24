using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Server.Persistence.Services
{
    public class JwtService
    {
        public static string GenerateToken(string Email)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ExampleSecurityKey"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(1);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, Email)
            };

            var token = new JwtSecurityToken("http://morfit.com", "http://morfit.com", claims, null, expiry, creds);

            String tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenStr;
        }
    }
}
