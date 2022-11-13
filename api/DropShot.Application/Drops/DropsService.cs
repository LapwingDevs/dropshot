using AutoMapper;
using AutoMapper.QueryableExtensions;
using DropShot.Application.Common;
using DropShot.Application.Drops.Events;
using DropShot.Application.Drops.Interfaces;
using DropShot.Application.Drops.Models;
using DropShot.Domain.Entities;
using DropShot.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DropShot.Application.Drops;

public class DropsService : IDropsService
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IAppDateTime _dateTime;
    private readonly IDropsListConverter _dropsListConverter;
    private readonly IMediator _mediator;

    public DropsService(
        IDbContext dbContext,
        IMapper mapper,
        IAppDateTime dateTime,
        IDropsListConverter dropsListConverter,
        IMediator mediator)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _dateTime = dateTime;
        _dropsListConverter = dropsListConverter;
        _mediator = mediator;
    }

    public async Task<DropDetailsDto> GetDropDetails(int dropId)
    {
        if (dropId < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(dropId));
        }

        var drop = await _dbContext.Drops
            .Include(d => d.DropItems)
            .ThenInclude(i => i.Variant)
            .ThenInclude(v => v.Product)
            .SingleOrDefaultAsync(d => d.Id == dropId);

        if (drop is null)
        {
            throw new Exception($"Cannot find drop with id {dropId}");
        }

        return new DropDetailsDto()
        {
            Id = drop.Id,
            Description = drop.Description,
            Name = drop.Name,
            StartDateTime = drop.StartDateTime,
            EndDateTime = drop.EndDateTime,
            DropItems = drop.DropItems
                .Where(i => i.Status == DropItemStatus.Available)
                .Select(x => new DropItemDto()
                {
                    DropItemId = x.Id,
                    VariantId = x.VariantId,
                    ProductId = x.Variant.ProductId,
                    ProductName = x.Variant.Product.Name,
                    UnitOfSize = x.Variant.Product.UnitOfSize,
                    Size = x.Variant.Size
                }).ToList()
        };
    }

    public async Task<DropsLandingPageVm> GetDrops()
    {
        var drops = await _dbContext.Drops
            .Where(d => d.EndDateTime > _dateTime.Now)
            .ProjectTo<DropCardDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return _dropsListConverter.ConvertDropsListToLandingPageVm(drops);
    }

    public async Task<IEnumerable<AdminDropDto>> GetDropsWithDetails() =>
        await _dbContext.Drops
            .Where(d => d.EndDateTime > _dateTime.Now)
            .ProjectTo<AdminDropDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

    public async Task AddDrop(AddDropRequest request)
    {
        if (request.DropItems.Any() == false)
        {
            throw new Exception("Cannot add drop without items");
        }

        if (request.StartDateTime > request.EndDateTime)
        {
            throw new Exception("Cannot set start date after end date");
        }

        var drop = new Drop()
        {
            Name = request.Name,
            Description = request.Description,
            StartDateTime = request.StartDateTime,
            EndDateTime = request.EndDateTime,
            DropItems = request.DropItems.Select(i =>
                new DropItem()
                {
                    // Quantity = i.Quantity,
                    VariantId = i.VariantId
                }).ToList()
        };

        await _dbContext.Drops.AddAsync(drop);
        await SetVariantsStatusAsInDrop(request);

        await _dbContext.SaveChangesAsync(CancellationToken.None);

        await _mediator.Publish(new DropIsCreatedEvent(drop));
    }

    private async Task SetVariantsStatusAsInDrop(AddDropRequest request)
    {
        var variantsAddedToDrop = await _dbContext.Variants
            .Where(v => request.DropItems.Select(x => x.VariantId).Contains(v.Id))
            .ToListAsync();

        foreach (var variant in variantsAddedToDrop)
        {
            variant.Status = VariantStatus.Drop;
        }
    }
}