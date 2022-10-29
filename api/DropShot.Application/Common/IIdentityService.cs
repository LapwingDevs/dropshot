using DropShot.Application.Auth.Models;

namespace DropShot.Application.Common;

public interface IIdentityService
{
    Task<(Result Result, string firstName, string lastName, string Email, string Id)> RegisterUser(string email,
        string firstName, string lastName, string password);

    Task<Result> DeleteUser(string userId, string email);
    
    Task<(JWTAuthorizationResult Result, string userId)> LoginUser(string email, string password);

    Task<Result> LogoutUser(string userId, string email);

    Task<bool> CheckLogout(string userId);

    Task<(Result Result, string UserName, string Email)> CheckToken(string token);

    Task<(Result, IEnumerable<string> admins)> GetAdmins();
    
    Task<JWTAuthorizationResult> RefreshToken(string userId, string email);

    Task<Result> PromoteUserToAdmin(string userEmail);
    
    Task<Result> DegradeUser(string userEmail);
}