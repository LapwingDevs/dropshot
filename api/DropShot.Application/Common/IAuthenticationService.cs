using DropShot.Application.Auth.Models;

namespace DropShot.Application.Common;

public interface IAuthenticationService
{
    Task<(JWTAuthorizationResult Result, string userId)> LoginUser(string email, string password);

    Task<Result> LogoutUser(string userId, string email);
    
    Task<JWTAuthorizationResult> RefreshToken(string token);
}