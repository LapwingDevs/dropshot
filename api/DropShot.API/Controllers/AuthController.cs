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

    [HttpPost("login")]
    public async Task<LoginUserResponse> LoginUser(LoginUserRequest loginUserRequest)
    {
        return await _authService.LoginUser(loginUserRequest);
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