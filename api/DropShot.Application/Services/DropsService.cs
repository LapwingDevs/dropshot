using DropShot.Application.Common;
using DropShot.Application.Interfaces;
using DropShot.Application.Models.Drops;
using Microsoft.EntityFrameworkCore;

namespace DropShot.Application.Services;

public class DropsService : IDropsService
{
    private readonly IDbContext _dbContext;

    public DropsService(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DropsVm> GetDrops()
    {
        var drops = await _dbContext.Drops.ToListAsync();
        return new DropsVm();
    }

    public Task AddDrop(AddDropRequest request)
    {
        throw new NotImplementedException();
    }
}