namespace DropShot.Application.Users.Models;

public class UserVm
{
    public IReadOnlyCollection<UserDto> Users { get; set; }

    public int Count { get; set; }
}