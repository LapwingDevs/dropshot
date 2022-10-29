using DropShot.Application.Auth.Models;
using DropShot.Application.Users.Models;

namespace DropShot.Application.Auth.Interfaces;

public interface IAuthService
{
    Task<RegisterResponseDto> RegisterUser(RegisterUserDto registerUserDto);
    Task<LoginUserResponse> LoginUser(LoginUserRequest loginUserRequest);

    Task<IEnumerable<UserDto>> GetAdmins();

    Task<Result> PromoteUser(string email);

    Task<Result> DegradeUser(string email);
}