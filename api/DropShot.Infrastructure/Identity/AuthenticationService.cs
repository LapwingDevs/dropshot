using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DropShot.Application.Auth.Models;
using DropShot.Application.Common;
using DropShot.Application.Common.Abstraction;
using DropShot.Infrastructure.Identity.Extensions;
using DropShot.Infrastructure.Identity.Helpers;
using DropShot.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DropShot.Infrastructure.Identity;

public class AuthenticationService : IAuthenticationService
{
    private const string TokenName = "Refresh";
    
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
    private readonly AuthSettings _authSettings;

    public AuthenticationService(
        UserManager<ApplicationUser> userManager,
        IPasswordHasher<ApplicationUser> passwordHasher,
        IOptions<AuthSettings> authSettings)
    {
        _userManager = userManager;
        _authSettings = authSettings.Value;
        _passwordHasher = passwordHasher;
    }

    public async Task<(JWTAuthorizationResult Result, string refreshToken, string userId)> LoginUser(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            return (JWTAuthorizationResult.Failure(new[] {"Email not found"}), "", "");

        var signResult = await _userManager.CheckPasswordAsync(user, password);

        if (!signResult)
        {
            return (JWTAuthorizationResult.Failure(new[] {"Wrong password"}), "", "");
        }

        var accessToken = await CreateAccessToken(user);
        var refreshToken = CreateRefreshToken(user);

        var result =  await _userManager.SetAuthenticationTokenAsync(user, user.Email, TokenName, refreshToken);
        
        if (!result.Succeeded)
        {
            return (JWTAuthorizationResult.Failure(new[] {"Something went wrong"}), "", "");
        }
        
        return (JWTAuthorizationResult.Success(accessToken), refreshToken, user.Id);
    }

    public async Task<Result> LogoutUser(string userId, string email)
    {
        var userById = await _userManager.FindByIdAsync(userId);
        var userByEmail = await _userManager.FindByEmailAsync(email);

        if (userByEmail == null || userById == null)
        {
            return Result.Failure(new List<string> {"User not found"});
        }

        if (userById.Id != userByEmail.Id)
        {
            return Result.Failure(new List<string> {"User does not match token"});
        }

        var result = await _userManager.SetAuthenticationTokenAsync(userByEmail, userByEmail.Email, TokenName,
            _authSettings.LogoutToken);
        return result.ToApplicationResult();
    }

    public async Task<(JWTAuthorizationResult, string refreshToken)> RefreshToken(string token)
    {
        var validationResult = await CheckRefreshToken(token);

        if (!validationResult.valid || validationResult.user == null)
        {
            return (JWTAuthorizationResult.Failure(new List<string> {"User does not match token"}), "");
        }

        var accessToken = await CreateAccessToken(validationResult.user);
        var refreshToken = CreateRefreshToken(validationResult.user);

        await _userManager.SetAuthenticationTokenAsync(validationResult.user, validationResult.user.Email, TokenName,
            refreshToken);

        return (JWTAuthorizationResult.Success(accessToken), refreshToken);
    }

    private async Task<(bool valid, ApplicationUser? user)> CheckRefreshToken(string token)
    {
        var allUsers = await _userManager.Users.ToListAsync();
        for (int i = 0; i < allUsers.Count; i++)
        {
            var userToken = await _userManager.GetAuthenticationTokenAsync(allUsers[i], allUsers[i].Email, TokenName);
            if (token == userToken)
            {
                return (true, allUsers[i]);
            }
        }

        return (false, null);
    }

    private string CreateRefreshToken(ApplicationUser user)
    {
        return _passwordHasher.HashPassword(user, Guid.NewGuid().ToString())
            .Replace("+", string.Empty)
            .Replace("=", string.Empty)
            .Replace("/", string.Empty);
    }

    private async Task<string> CreateAccessToken(ApplicationUser user)
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.AuthKey));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
            issuer: _authSettings.Issuer,
            audience: _authSettings.Audience,
            claims: await GetTokenClaims(user),
            notBefore: DateTime.Now,
            expires: DateTime.Now.Add(TimeSpan.FromSeconds(_authSettings.Expire)),
            signingCredentials: signingCredentials
        ));

        return token;
    }

    private async Task<IEnumerable<Claim>> GetTokenClaims(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString())
        };

        var userClaims = await _userManager.GetClaimsAsync(user);
        claims.AddRange(userClaims);

        var userRoles = await _userManager.GetRolesAsync(user);

        foreach (var useRole in userRoles)
        {
            claims.Add(new Claim("role", useRole));
        }

        return claims;
    }
}