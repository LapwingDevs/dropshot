﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using DropShot.Application.Common;
using DropShot.Application.Drops.Interfaces;
using DropShot.Application.Drops.Models;
using DropShot.Domain.Entities;
using DropShot.Domain.Enums;
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

    public async Task<DropDetailsDto> GetDropDetails(int dropId)
    {
        if (dropId < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(dropId));
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

    public async Task<DropsLandingPageVm> GetDrops()
    {
        var drops = await _dbContext.Drops
            .Where(d => d.EndDateTime > _dateTime.Now)
            .ProjectTo<DropCardDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return _dropsListConverter.ConvertDropsListToLandingPageVm(drops);
    }

    public async Task<IEnumerable<DropDetailsDto>> GetDropsWithDetails() =>
        await _dbContext.Drops
            .Include(d => d.DropItems)
            .Where(d => d.EndDateTime > _dateTime.Now)
            .ProjectTo<DropDetailsDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

    public async Task AddDrop(AddDropRequest request)
    {
        var drop = new Drop()
        {
            Name = request.Name,
            Description = request.Description,
            StartDateTime = request.StartDateTime,
            EndDateTime = request.EndDateTime,
            DropItems = request.DropItems.Select(i =>
                new DropItem()
                {
                    Quantity = i.Quantity,
                    VariantId = i.VariantId
                }).ToList()
        };
        
        await _dbContext.Drops.AddAsync(drop);
        await SetVariantsStatusAsInDrop(request);
        
        await _dbContext.SaveChangesAsync(CancellationToken.None);
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