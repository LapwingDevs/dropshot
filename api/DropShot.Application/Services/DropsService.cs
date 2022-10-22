using AutoMapper;
using AutoMapper.QueryableExtensions;
using DropShot.Application.Common;
using DropShot.Application.Interfaces;
using DropShot.Application.Models.Drops;
using Microsoft.EntityFrameworkCore;

namespace DropShot.Application.Services;

public class DropsService : IDropsService
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public DropsService(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<DropsVm> GetDrops()
    {
        var dateNow = DateTime.Now;

        var drops = await _dbContext.Drops
            .Where(d => d.EndDateTime > dateNow)
            .ProjectTo<DropCardDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        var activeDrops = new List<DropCardDto>();
        var incomingDrops = new List<DropCardDto>();

        foreach (var drop in drops)
        {
            if (drop.StartDateTime < dateNow)
            {
                activeDrops.Add(drop);
            }
            else
            {
                activeDrops.Add(drop);
            }
        }

        return new DropsVm()
        {
            ActiveDrops = activeDrops,
            IncomingDrops = incomingDrops
        };
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

    public Task AddDrop(AddDropRequest request)
    {
        throw new NotImplementedException();
    }
}