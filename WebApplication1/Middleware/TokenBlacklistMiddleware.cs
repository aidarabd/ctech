using WebApplication1.Services;

namespace WebApplication1.Middleware;

public class TokenBlacklistMiddleware
{
    private readonly RequestDelegate _next;
    private readonly TokenBlacklistService _blacklistService;

    public TokenBlacklistMiddleware(RequestDelegate next, TokenBlacklistService blacklistService)
    {
        _next = next;
        _blacklistService = blacklistService;
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (!string.IsNullOrEmpty(token) && _blacklistService.IsTokenBlacklisted(token))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }

        await _next(context);
    }
}