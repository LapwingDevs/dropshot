using DropShot.Application.Common.AutoMapper;

namespace DropShot.Application.Users.Models;

public class CreateUserRequest : IMapFrom<Domain.Entities.User>
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }

    public string Email { get; set; }

    public string ApplicationUserId { get; set; }

    public AddressDto Address { get; set; }
}