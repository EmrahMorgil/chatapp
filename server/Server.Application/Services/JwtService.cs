using Microsoft.IdentityModel.Tokens;
using Server.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Server.Persistence.Services;

public class JwtService
{
    public static string GenerateToken(Guid Id)
    {
        var settings = Configuration.GetSettings<Settings>("TokenInfo");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiry = DateTime.Now.AddDays(settings.ValidityPeriod);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, Id.ToString())
        };

        var token = new JwtSecurityToken(settings.ValidAudience, settings.ValidIssuer, claims, null, expiry, creds);

        String tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
        String bearerToken = "Bearer " + tokenStr;
        return bearerToken;
    }
    public static JwtSecurityToken DecodeToken(string authorizationHeader)
    {
        string jwtToken = authorizationHeader.Replace("Bearer ", "");
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.ReadJwtToken(jwtToken);
    }
}
