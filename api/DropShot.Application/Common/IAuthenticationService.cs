using DropShot.Application.Auth.Models;

namespace DropShot.Application.Common;

public interface IAuthenticationService
{
    Task<(JWTAuthorizationResult Result, string refreshToken, string userId)> LoginUser(string email, string password);

    Task<Result> LogoutUser(string userId, string email);

    Task<(JWTAuthorizationResult, string refreshToken)> RefreshToken(string token);
}