using AutoMapper;
using DropShot.Application.Auth.Interfaces;
using DropShot.Application.Auth.Models;
using DropShot.Application.Common;
using DropShot.Application.User.Interfaces;
using DropShot.Application.Users.Models;
using DropShot.Domain.Entities;

namespace DropShot.Application.Auth;

public class AuthService : IAuthService
{
    private readonly IAuthenticationService _authenticationService;
    private readonly ITokenManager _tokenManager;
    private readonly IIdentityService _identityService;
    private readonly IUserService _userService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public AuthService(
        IIdentityService identityService,
        IUserService userService,
        IMapper mapper, IAuthenticationService authenticationService, ITokenManager tokenManager,
        ICurrentUserService currentUserService)
    {
        _identityService = identityService;
        _userService = userService;
        _mapper = mapper;
        _authenticationService = authenticationService;
        _tokenManager = tokenManager;
        _currentUserService = currentUserService;
    }

    public async Task<RegisterResponseDto> RegisterUser(RegisterUserDto registerUserDto)
    {
        var (result, firstName, lastName, email, id) =
            await _identityService.RegisterUser(registerUserDto.Email, registerUserDto.FirstName,
                registerUserDto.LastName, registerUserDto.Password);

        var domainUser = new UserDto();

        if (result.Succeeded)
        {
            domainUser = await _userService.CreateUser(new()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                ApplicationUserId = id,
                Address = registerUserDto.Address
            });

            if (domainUser == null)
            {
                await _identityService.DeleteUser(id, email);
                result = Result.Failure(new List<string> {"Can not create user"});
            }
        }

        return new RegisterResponseDto
        {
            User = domainUser ?? null,
            Errors = new List<string>(result.Errors)
        };
    }

    public async Task<LoginUserResponse> LoginUser(LoginUserRequest loginUserRequest)
    {
        var (result, appUserId) =
            await _authenticationService.LoginUser(loginUserRequest.Email, loginUserRequest.Password);

        if (!result.Succeeded)
        {
            return new LoginUserResponse {Token = "", RefreshToken = "", Errors = result.Errors};
        }

        var user = await _userService.GetUser(u => u.ApplicationUserId == appUserId);

        return new LoginUserResponse
            {Token = result.Token, RefreshToken = result.RefreshToken, User = user, Errors = result.Errors};
    }

    public async Task<Result> LogoutUser(string email)
    {
        var result = await _authenticationService.LogoutUser(_currentUserService.UserId, email);

        if (result.Succeeded)
        {
            await _tokenManager.DeactivateCurrent();
        }

        return result;
    }

    public async Task<JWTAuthorizationResult> RefreshToken(string token)
    {
        return await _authenticationService.RefreshToken(token);
    }

    public async Task<Result> ChangePassword(ChangePasswordRequest changePasswordRequest)
    {
        var result = await _identityService.ChangePassword(_currentUserService.UserId, changePasswordRequest.Email,
            changePasswordRequest.OldPassword, changePasswordRequest.NewPassword);

        if (result.Succeeded)
        {
            await _tokenManager.DeactivateCurrent();
        }

        return result;
    }

    public async Task<IEnumerable<UserDto>> GetAdmins()
    {
        var users = new List<UserDto>();
        var (result, admins) = await _identityService.GetAdmins();

        if (!result.Succeeded)
        {
            throw new Exception(string.Join(" ", result.Errors));
        }

        foreach (var admin in admins)
        {
            users.Add(await _userService.GetUser(u => u.ApplicationUserId == admin));
        }

        return users;
    }

    public async Task<Result> PromoteUser(string email)
    {
        var result = await _identityService.PromoteUserToAdmin(email);

        if (!result.Succeeded)
        {
            throw new Exception(string.Join(" ", result.Errors));
        }

        return result;
    }

    public async Task<Result> DegradeUser(string email)
    {
        var result = await _identityService.DegradeUser(email);

        if (!result.Succeeded)
        {
            throw new Exception(string.Join(" ", result.Errors));
        }

        return result;
    }
}