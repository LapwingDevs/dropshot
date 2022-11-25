using System.Linq.Expressions;
using DropShot.Application.Users.Models;

namespace DropShot.Application.User.Interfaces;

public interface IUserService
{
    Task<UserDto> CreateUser(CreateUserRequest createUserRequest);
    Task<UserDto> UpdateUser(UpdateUserDto updateUserDto);
    Task<UserDto> GetUser(Expression<Func<Domain.Entities.User, bool>> predicate);
    Task<UserVm> FindUsers(string term);
}