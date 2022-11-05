using System.IdentityModel.Tokens.Jwt;
using DropShot.Application.Common;
using DropShot.Infrastructure.Identity.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace DropShot.Infrastructure.Identity;

public class TokenManager : ITokenManager
{
    private readonly IDistributedCache _cache;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IIdentityService _identityService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IOptions<AuthSettings> _authSettings;
    

    public TokenManager(
        IHttpContextAccessor httpContextAccessor, 
        IIdentityService identityService, 
        ICurrentUserService currentUserService, 
        IDistributedCache cache, 
        IOptions<AuthSettings> authSettings)
    {
        _httpContextAccessor = httpContextAccessor;
        _identityService = identityService;
        _currentUserService = currentUserService;
        _cache = cache;
        _authSettings = authSettings;
    }
    
    public async Task<bool> IsCurrentActiveToken()
        => await IsActive(GetCurrent());

    public async Task DeactivateCurrent()
        => await Deactivate(GetCurrent());

    public async Task<bool> IsActive(string token)
        => await _cache.GetStringAsync(GetKey(token)) == null;

    
    public async Task<bool> IsInCorrectRole()
    {
        var currentToken = GetCurrent();
        
        if (string.IsNullOrEmpty(currentToken))
        {
            return true;
        }
        
        var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(currentToken);
        var role = decodedToken.Claims.First(c => c.Type == "role");

        var (result, userRoles) = await _identityService.GetUserRolesById(_currentUserService.UserId);

        if (!result.Succeeded || !userRoles.Contains(role.Value))
        {
            return false;
        }

        return true;
    }
    
    private async Task Deactivate(string token)
        => await _cache.SetStringAsync(GetKey(token),
            " ", new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow =
                    TimeSpan.FromMinutes(_authSettings.Value.Expire)
            });

    private string GetCurrent()
    {
        var authorizationHeader =
            _httpContextAccessor.HttpContext?.Request.Headers["authorization"] ?? StringValues.Empty;

        return authorizationHeader == StringValues.Empty
            ? string.Empty
            : authorizationHeader.Single().Split(" ").Last();
    }
    
    private static string GetKey(string token)
        => $"tokens:{token}:deactivated";
}