using System.Linq.Expressions;
using System.Text.RegularExpressions;
using AutoMapper;
using DropShot.Application.Common;
using DropShot.Application.Users.Interfaces;
using DropShot.Application.Common.Abstraction;
using DropShot.Application.Users.Interfaces;
using DropShot.Application.Users.Models;
using DropShot.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DropShot.Application.Users;

public class UserService : IUserService
{
    private readonly IIdentityService _identityService;
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public UserService(
        IDbContext dbContext,
        IMapper mapper, IIdentityService identityService)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _identityService = identityService;
    }
    
    public async Task<UserDto> CreateUser(CreateUserRequest createUserRequest)
    {
        var user = _mapper.Map<Domain.Entities.User>(createUserRequest);
        
        await _dbContext.Users.AddAsync(user);

        await _dbContext.SaveChangesAsync(new CancellationToken());
        
        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> UpdateUser(UpdateUserDto updateUserDto)
    {
        var user = _mapper.Map<Domain.Entities.User>(updateUserDto);

        var existingUser = await _dbContext.Users.FindAsync(updateUserDto.Id);
        if (existingUser == null)
        {
            throw new ArgumentException($"User with id {updateUserDto.Id} not found!");
        }

        _mapper.Map(updateUserDto, existingUser);

        await _dbContext.SaveChangesAsync(new CancellationToken());



        return _mapper.Map<UserDto>(existingUser);
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

    public async Task<UserVm> FindUsers(string term, bool usersOnly)
    {
        if (string.IsNullOrWhiteSpace(term))
        {
            throw new ArgumentException("Term can not be empty");
        }
        
        var usersByUserName = await _dbContext.Users
            .Where(u => u.FirstName.Contains(term) || u.LastName.Contains(term))
            .ToListAsync();

        term = term.Contains("@") ? term.Split("@")[0] : term;
        var rx = new Regex(@"^([^@]+)");
            
        var usersByUserEmail = await _dbContext.Users
            .Where(u => !usersByUserName.Select(i => i.Id).Contains(u.Id))
            .ToListAsync();

        usersByUserEmail = usersByUserEmail.Where(u => rx.Match(u.Email).Success && rx.Match(u.Email).Value.Contains(term)).ToList();

        if (usersOnly)
        {
            var result = await _identityService.GetAdmins();

            usersByUserName = usersByUserName.Where(u => !result.admins.Contains(u.ApplicationUserId)).ToList();
            usersByUserEmail = usersByUserEmail.Where(u => !result.admins.Contains(u.ApplicationUserId)).ToList();
        }

        var users = new List<UserDto>();
            
        users.AddRange(_mapper.Map<List<User>, List<UserDto>>(usersByUserEmail));
        users.AddRange(_mapper.Map<List<User>, List<UserDto>>(usersByUserName));

        return new()
        {
            Users = users,
            Count = users.Count
        };
    }
    
}
