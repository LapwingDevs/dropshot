using DropShot.Application.Common;
using DropShot.Application.Users.Interfaces;
using DropShot.Application.Common.Abstraction;
using DropShot.Application.Users.Interfaces;
using DropShot.Application.Users.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DropShot.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ICurrentUserService _currentUserService;

    public UserController(
        IUserService userService, 
        ICurrentUserService currentUserService)
    {
        _userService = userService;
        _currentUserService = currentUserService;
    }

    [HttpGet("loggedUser")]
    [Authorize]
    public async Task<UserDto> GetLoggedUser()
    {
        return await _userService.GetUser(u => u.ApplicationUserId == _currentUserService.UserId);
    }

    [HttpPut]
    [Authorize]
    public async Task<UserDto> UpdateUser(UpdateUserDto updateUserDto)
    {
        return await _userService.UpdateUser(updateUserDto);
    }

    [HttpGet("users")]
    [Authorize(Roles = "Admin")]
    public async Task<UserVm> FindUsers(string term, bool usersOnly = false)
    {
        return await _userService.FindUsers(term, usersOnly);
    }

}
