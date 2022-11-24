using DropShot.Application.Auth.Models;
using DropShot.Application.Common;
using DropShot.Application.Common.Abstraction;
using DropShot.Infrastructure.Identity.Extensions;
using DropShot.Infrastructure.Identity.Models;

using Microsoft.AspNetCore.Identity;

namespace DropShot.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<(Result Result, string firstName, string lastName, string Email, string Id)> RegisterUser(string email, string firstName, string lastName, string password)
    {
        var user = new ApplicationUser
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            UserName = firstName + lastName + Guid.NewGuid().ToString("N")
        };

        var result = await _userManager.CreateAsync(user, password);

        string firstNameResponse = "";
        string lastNameResponse = "";
        string emailResponse = "";
        string idResponse = "";
        
        if (result.Succeeded)
        {
            var userCreated = await _userManager.FindByEmailAsync(email);

            await AddRole(userCreated, Roles.User);
            
            firstNameResponse = firstName;
            lastNameResponse = lastName;
            emailResponse = email;
            idResponse = userCreated.Id;
        }
        
        return (result.ToApplicationResult(), firstNameResponse, lastNameResponse, emailResponse, idResponse);
    }
    
    public async Task<Result> DeleteUser(string userId, string email)
    {
        var userById = await _userManager.FindByIdAsync(userId);
        var userByEmail = await _userManager.FindByEmailAsync(email);

        if (userByEmail == null || userById == null)
        {
            return Result.Failure(new List<string> { "User not found" });
        }

        if (userById.Id != userByEmail.Id)
        {
            return Result.Failure(new List<string> { "User does not match token" });
        }

        if (userById.Id == userByEmail.Id)
        {
            var result = await _userManager.DeleteAsync(userByEmail);
            return result.ToApplicationResult();
        }

        return Result.Failure(new List<string> { "Wrong data" });
    }
    
    public async Task<Result> ChangePassword(string userId, string email, string oldPassword, string newPassword)
    {
        var userById = await _userManager.FindByIdAsync(userId);
        var userByEmail = await _userManager.FindByEmailAsync(email);

        if (userByEmail == null || userById == null)
        {
            return (Result.Failure(new List<string> { "User not found" }));
        }

        if (userById.Id != userByEmail.Id)
        {
            return (Result.Failure(new List<string> { "User does not match token" }));
        }

        var result = await _userManager.ChangePasswordAsync(userByEmail, oldPassword, newPassword);
        return result.ToApplicationResult();
    }

    public async Task<(Result, IEnumerable<string>)> GetUserRolesById(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        
        if (user == null)
        {
            return (Result.Failure(new List<string>{ "User not found" }), Enumerable.Empty<string>());
        }

        return (Result.Success(), await _userManager.GetRolesAsync(user));
    }

    public async Task<(Result, IEnumerable<string> admins)> GetAdmins()
    {
        var admins = await _userManager.GetUsersInRoleAsync(Roles.Admin);

        if (admins == null)
        {
            return (Result.Failure(new List<string> {"Something went wrong"}), Enumerable.Empty<string>());
        }

        return (Result.Success(), admins.Select(u => u.Id));
    }

    public async Task<Result> PromoteUserToAdmin(string userEmail)
    {
        //TODO: Move it to the method get roles by email
        var user = await _userManager.FindByEmailAsync(userEmail);

        if (user == null)
        {
            return Result.Failure(new List<string>{ "User not found" });
        }

        var userRoles = await _userManager.GetRolesAsync(user);

        if (userRoles.Contains(Roles.Admin))
        {
            return Result.Failure(new List<string>{ "User already is an admin"});
        }

        if (userRoles.Contains(Roles.User))
        {
            await RemoveRole(user, Roles.User);
        }

        await AddRole(user, Roles.Admin);
        
        return Result.Success();
    }

    public async Task<Result> DegradeUser(string userEmail)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);

        if (user == null)
        {
            return Result.Failure(new List<string>{ "User not found" });
        }
        
        var userRoles = await _userManager.GetRolesAsync(user);

        if (!userRoles.Contains(Roles.Admin))
        {
            return Result.Failure(new List<string>{ "User is not an admin" });
        }

        if (!userRoles.Contains(Roles.User))
        {
            await AddRole(user, Roles.User);
        }

        await RemoveRole(user, Roles.Admin);

        return Result.Success();
    }

    private async Task AddRole(ApplicationUser user, string role)
    {
        if (!await _roleManager.RoleExistsAsync(role))
        {
            var roleResult = await _roleManager.CreateAsync(new IdentityRole(role));

            if (!roleResult.Succeeded)
            {
                throw new Exception("Failed to create role.");
            }
        }

        var result = await _userManager.AddToRoleAsync(user, role);

        if (!result.Succeeded)
        {
            throw new Exception("Failed to add user to role");
        }

    }

    private async Task RemoveRole(ApplicationUser user, string role)
    {
        if (!await _roleManager.RoleExistsAsync(role))
        {
            throw new Exception("Role does not exist.");
        }

        var result = await _userManager.RemoveFromRoleAsync(user, role);
        
        if (!result.Succeeded)
        {
            throw new Exception("Failed to remove users role");
        }
    }
}