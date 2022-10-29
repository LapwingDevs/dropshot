using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using DropShot.Application.Auth.Models;
using DropShot.Application.Common;
using DropShot.Infrastructure.Identity.Extensions;
using DropShot.Infrastructure.Identity.Helpers;
using DropShot.Infrastructure.Identity.Models;
using Duende.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;

namespace DropShot.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly AuthSettings _authSettings;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<AuthSettings> authSettings)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _authSettings = authSettings.Value;
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

    
    public async Task<(JWTAuthorizationResult Result, string userId)> LoginUser(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            return (JWTAuthorizationResult.Failure(new string[] { "Email not found" }), "");

        /*if (!user.EmailConfirmed)
            return (JWTAuthorizationResult.Failure(new string[] { "Email not confirmed" }), "", "");*/

        var signResult = await _userManager.CheckPasswordAsync(user, password);

        if (signResult)
        {
            var apiResult = await CreateToken(user);
            await _userManager.SetAuthenticationTokenAsync(user, user.Email, "JWT", apiResult.Token);
            return (apiResult, user.Id);
        }

        return (JWTAuthorizationResult.Failure(new string[] { "Wrong password" }), "");
        
    }
    
    public async Task<Result> LogoutUser(string userId, string email)
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
            
        if (userById.Id == userByEmail.Id)
        {
            var result = await _userManager.SetAuthenticationTokenAsync(userByEmail, userByEmail.Email, "JWT", _authSettings.LogoutToken);
            return result.ToApplicationResult();
        }

        return (Result.Failure(new List<string> { "Wrong data" }));
    }
    
    public async Task<bool> CheckLogout(string userId)
    {
        if (string.IsNullOrEmpty(userId))
        {
            return true;
        }

        var userById = await _userManager.FindByIdAsync(userId);
        var userToken = await _userManager.GetAuthenticationTokenAsync(userById, userById.Email, "JWT");

        if (userToken == _authSettings.LogoutToken)
        {
            return false;
        }
        return true;
    }
    
    public async Task<(Result Result, string UserName, string Email)> CheckToken(string token)
    {
        var allUsers = await _userManager.Users.ToListAsync();
        for (int i = 0; i < allUsers.Count; i++)
        {
            var userToken = await _userManager.GetAuthenticationTokenAsync(allUsers[i], allUsers[i].Email, "JWT");
            if (token == userToken)
            {
                return (Result.Success(), allUsers[i].UserName, allUsers[i].Email);
            }
        }

        return (Result.Failure(new List<string> { "User not found" }), "", "");
    }
    
    public async Task<JWTAuthorizationResult> RefreshToken(string userId, string email)
    {
        var userById = await _userManager.FindByIdAsync(userId);
        var userByEmail = await _userManager.FindByEmailAsync(email);

        if (userByEmail == null || userById == null)
        {
            return (JWTAuthorizationResult.Failure(new List<string> { "User not found" }));
        }

        if (userById.Id != userByEmail.Id)
        {
            return (JWTAuthorizationResult.Failure(new List<string> { "User does not match token" }));
        }

        if (userById.Id == userByEmail.Id)
        {
            var apiResult = await CreateToken(userByEmail);
            await _userManager.SetAuthenticationTokenAsync(userByEmail, userByEmail.Email, "JWT", apiResult.Token);
            return apiResult;
        }
        
        return (JWTAuthorizationResult.Failure(new List<string> { "Wrong data" }));
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

    private async Task<JWTAuthorizationResult> CreateToken(ApplicationUser user)
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.AuthKey));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var apiResult = JWTAuthorizationResult.Success(new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
            issuer: _authSettings.Issuer,
            audience: _authSettings.Audience,
            claims: await GetTokenClaims(user),
            notBefore: DateTime.Now,
            expires: DateTime.Now.Add(TimeSpan.FromSeconds(_authSettings.Expire)),
            signingCredentials: signingCredentials
        )));

        return apiResult;
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