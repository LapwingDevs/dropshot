using AutoMapper;
using DropShot.Application.Users.Models;
using DropShot.Domain.Entities;

namespace DropShot.Application.Common.AutoMapper;

public class TempMappingProfile : Profile
{
    public TempMappingProfile()
    {
        CreateMap<AddressDto, Address>().ReverseMap();
        CreateMap<UserDto, Domain.Entities.User>().ReverseMap();
        CreateMap<CreateUserRequest, Domain.Entities.User>().ReverseMap();
        CreateMap<UpdateUserDto, Domain.Entities.User>()
            .ForMember(m => m.Email, s => s.Ignore())
            .ReverseMap();
    }
}