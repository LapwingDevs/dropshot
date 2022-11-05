using System.Net;
using DropShot.Application.Common;

namespace DropShot.API.Middleware;

public class TokenManagerMiddleware : IMiddleware
{
    private readonly ITokenManager _tokenManager;

    public TokenManagerMiddleware(ITokenManager tokenManager)
    {
        _tokenManager = tokenManager;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (await _tokenManager.IsCurrentActiveToken() && await _tokenManager.IsInCorrectRole())
        {
            await next(context);

            return;
        }
        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
    }
}