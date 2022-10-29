using System.Linq.Expressions;
using AutoMapper;
using DropShot.Application.Common;
using DropShot.Application.User.Interfaces;
using DropShot.Application.Users.Models;
using Microsoft.EntityFrameworkCore;

namespace DropShot.Application.Users;

public class UserService : IUserService
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public UserService(
        IDbContext dbContext,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<UserDto> CreateUser(CreateUserRequest createUserRequest)
    {
        var user = _mapper.Map<Domain.Entities.User>(createUserRequest);

        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync(new CancellationToken());

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> GetUser(Expression<Func<Domain.Entities.User,bool>> predicate)
    {
        var user = await _dbContext.Users
            .Include(u => u.Address)
            .SingleOrDefaultAsync(predicate);

        if (user == null)
        {
            throw new Exception("User not found");
        }

        return _mapper.Map<UserDto>(user);
    }
}