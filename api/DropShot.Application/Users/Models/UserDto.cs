using DropShot.Application.Common.AutoMapper;

namespace DropShot.Application.Users.Models;

public class UserDto : IMapFrom<Domain.Entities.User>
{
    public int  Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }

    public string Email { get; set; }
    
    public AddressDto AddressDto { get; set; }
}