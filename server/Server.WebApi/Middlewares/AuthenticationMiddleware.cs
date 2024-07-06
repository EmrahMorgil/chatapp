using Server.Shared.Services;
using Server.Shared.Variables;

namespace Server.WebApi.Middlewares;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var tokenInfo = JwtService.DecodeToken(context.Request.Headers["Authorization"]);

        if (tokenInfo != null)
        {
            Global.UserId = tokenInfo.UserId;
            Global.Email = tokenInfo.Email;
        }

        await _next(context);
    }
}
