using DropShot.Application.Users.Models;

namespace DropShot.Application.Auth.Models;

public class LoginUserResponse
{
    public string Token { get; set; }
    public UserDto User { get; set; }
    public IEnumerable<string> Errors { get; set; }
}