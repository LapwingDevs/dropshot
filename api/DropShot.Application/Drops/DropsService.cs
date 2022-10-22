using AutoMapper;
using AutoMapper.QueryableExtensions;
using DropShot.Application.Common;
using DropShot.Application.Drops.Interfaces;
using DropShot.Application.Drops.Models;
using Microsoft.EntityFrameworkCore;

namespace DropShot.Application.Drops;

public class DropsService : IDropsService
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IAppDateTime _dateTime;
    private readonly IDropsListConverter _dropsListConverter;

    public DropsService(
        IDbContext dbContext,
        IMapper mapper,
        IAppDateTime dateTime,
        IDropsListConverter dropsListConverter)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _dateTime = dateTime;
        _dropsListConverter = dropsListConverter;
    }

    public async Task<DropsLandingPageVm> GetDrops()
    {
        var drops = await _dbContext.Drops
            .Where(d => d.EndDateTime > _dateTime.Now)
            .ProjectTo<DropCardDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return _dropsListConverter.ConvertDropsListToLandingPageVm(drops);
    }

    public async Task<DropDetailsDto> GetDropDetails(int dropId)
    {
        if (dropId < 0)
        {
            throw new ArgumentOutOfRangeException("Drop id must be greater than 0");
        }

        var drop = await _dbContext.Drops
            .Include(d => d.DropItems)
            .SingleOrDefaultAsync(d => d.Id == dropId);

        if (drop is null)
        {
            throw new Exception($"Cannot find drop with id {dropId}");
        }

        return _mapper.Map<DropDetailsDto>(drop);
    }

    public async Task AddDrop(AddDropRequest request)
    {
        // var dateNow = DateTime.UtcNow;
        //
        // await _dbContext.Drops.AddRangeAsync(new[]
        // {
        //     new Drop()
        //     {
        //         Name = "Empty drop / incoming 1",
        //         Description = "Description",
        //         StartDateTime = dateNow.AddDays(3),
        //         EndDateTime = dateNow.AddDays(4)
        //     },
        //     new Drop()
        //     {
        //         Name = "Empty drop / incoming 1",
        //         Description = "Description",
        //         StartDateTime = dateNow.AddDays(5),
        //         EndDateTime = dateNow.AddDays(7)
        //     },
        //     new Drop()
        //     {
        //         Name = "Empty drop / active",
        //         Description = "Description",
        //         StartDateTime = dateNow.AddDays(-1),
        //         EndDateTime = dateNow.AddDays(1)
        //     }
        // });
        //
        // await _dbContext.SaveChangesAsync(CancellationToken.None);
    }
}