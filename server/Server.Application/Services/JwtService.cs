using Microsoft.IdentityModel.Tokens;
using Server.Domain.Entities;
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
            var settings = Configuration.GetSettings<Settings>("TokenInfo");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(settings.ValidityPeriod);
            
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, Email)
            };

            var token = new JwtSecurityToken(settings.ValidAudience, settings.ValidIssuer, claims, null, expiry, creds);

            String tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
            String bearerToken = "Bearer " + tokenStr;
            return bearerToken;
        }
    }
}
