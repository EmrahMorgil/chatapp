using Microsoft.IdentityModel.Tokens;
using Server.Application.Variables;
using Server.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Server.Persistence.Services;

public class JwtService
{
    public static string CreateToken(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
        };

        var expiry = DateTime.Now.AddDays(Global.ValidityPeriod);
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Global.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(Global.ValidAudience, Global.ValidIssuer, claims, null, expiry, creds);

        String tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
        String bearerToken = "Bearer " + tokenStr;
        return bearerToken;
    }
    public static DecodedTokenInfo DecodeToken(string authorizationHeader)
    {
        if (authorizationHeader == null)
        {
            return null!;
        }

        string jwtToken = authorizationHeader.Replace("Bearer ", "");
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        JwtSecurityToken token = tokenHandler.ReadJwtToken(jwtToken);

        var userId = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var email = token.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value;

        return new DecodedTokenInfo
        {
            UserId = Guid.Parse(userId),
            Email = email,
        };
    }
}
