using DropShot.Application.Users.Models;

namespace DropShot.Application.Auth.Models;

public class RegisterResponseDto
{
    public UserDto User { get; set; }
    
    public List<string> Errors { get; set; }
}