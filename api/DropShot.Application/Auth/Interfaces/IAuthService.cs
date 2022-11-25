using DropShot.Application.Auth.Models;
using DropShot.Application.Users.Models;

namespace DropShot.Application.Auth.Interfaces;

public interface IAuthService
{
    Task<RegisterResponseDto> RegisterUser(RegisterUserDto registerUserDto);
    
    Task<Result> ChangePassword(ChangePasswordRequest changePasswordRequest);

    Task<(LoginUserResponse response, string refreshToken)> LoginUser(LoginUserRequest loginUserRequest);
    
    Task<Result> LogoutUser(string email);

    Task<(JWTAuthorizationResult result, string refreshToken)> RefreshToken(string token);
    
    Task<IEnumerable<UserDto>> GetAdmins();

    Task<Result> PromoteUser(string email);

    Task<Result> DegradeUser(string email);
}