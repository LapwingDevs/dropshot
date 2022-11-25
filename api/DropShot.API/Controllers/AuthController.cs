using DropShot.API.Extensions;
using DropShot.Application.Auth.Interfaces;
using DropShot.Application.Auth.Models;
using DropShot.Application.Users.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DropShot.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("register")]
    public async Task<RegisterResponseDto> RegisterUser(RegisterUserDto registerUserDto)
    {
        return await _authService.RegisterUser(registerUserDto);
    }
    
    [HttpPost("changePassword")]
    [Authorize]
    public async Task<Result> ChangeUserPassword(ChangePasswordRequest changePasswordRequest)
    {
        return await _authService.ChangePassword(changePasswordRequest);
    }

    [HttpPost("login")]
    public async Task<LoginUserResponse> LoginUser(LoginUserRequest loginUserRequest)
    {
        var result = await _authService.LoginUser(loginUserRequest);

        Response.AddRefreshTokenCookie(result.refreshToken);

        return result.response;
    }

    [HttpPost("logout/{email}")]
    [Authorize]
    public async Task<Result> LogoutUser(string email)
    {
        return await _authService.LogoutUser(email);
    }

    [HttpPost("refreshToken")]
    public async Task<IActionResult> RefreshToken()
    {
        if (!Request.Cookies.TryGetValue("X-Refresh-Token", out var refreshToken))
        {
            return BadRequest();
        }
        
        var result = await _authService.RefreshToken(refreshToken);
        
        Response.AddRefreshTokenCookie(result.refreshToken);

        return Ok(result.result);
    }

    [HttpPost("promote/{email}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> PromoteUserToAdmin(string email)
    {
        await _authService.PromoteUser(email);
        
        return Ok();
    }
    
    [HttpPost("degrade/{email}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DegradeUser(string email)
    {
        await _authService.DegradeUser(email);
        
        return Ok();
    }


    [HttpGet("admins")]
    [Authorize(Roles = "Admin")]
    public async Task<IEnumerable<UserDto>> GetAdmins()
    {
        return await _authService.GetAdmins();
    }
}