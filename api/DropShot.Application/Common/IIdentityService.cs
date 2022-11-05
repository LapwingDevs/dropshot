using DropShot.Application.Auth.Models;

namespace DropShot.Application.Common;

public interface IIdentityService
{
    Task<(Result Result, string firstName, string lastName, string Email, string Id)> RegisterUser(string email,
        string firstName, string lastName, string password);

    Task<Result> DeleteUser(string userId, string email);

    Task<Result> ChangePassword(string userId, string email, string oldPassword, string newPassword);

    Task<(Result, IEnumerable<string>)> GetUserRolesById(string userId);
    
    Task<(Result, IEnumerable<string> admins)> GetAdmins();
    
    Task<Result> PromoteUserToAdmin(string userEmail);
    
    Task<Result> DegradeUser(string userEmail);
}