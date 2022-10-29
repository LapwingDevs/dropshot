using DropShot.Application.Common;
using Microsoft.Extensions.Primitives;

namespace DropShot.API.Middleware;

public class TokenManager : ITokenManager
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IIdentityService _identityService;
    private readonly ICurrentUserService _currentUserService;

    public TokenManager(
        IHttpContextAccessor httpContextAccessor, IIdentityService identityService, ICurrentUserService currentUserService
    )
    {
        _httpContextAccessor = httpContextAccessor;
        _identityService = identityService;
        _currentUserService = currentUserService;
    }

    public async Task<bool> IsCurrentActiveToken()
        => await IsActiveAsync(GetCurrentAsync());

    public async Task<bool> IsActiveAsync(string token)
    {
        return await _identityService.CheckLogout(_currentUserService.UserId);
    }

    private string GetCurrentAsync()
    {
        var authorizationHeader = _httpContextAccessor.HttpContext?.Request.Headers["authorization"] ?? StringValues.Empty;

        return authorizationHeader == StringValues.Empty
            ? string.Empty
            : authorizationHeader.Single().Split(" ").Last();
    }
}