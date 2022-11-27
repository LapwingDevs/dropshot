using DropShot.Application.Common;
using DropShot.Application.Common.Abstraction;
using DropShot.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace DropShot.Infrastructure.BackgroundServices.DeadlinesHandler.ExpiredDeadlinesCleaner;

internal class ExpiredDeadlinesCleaner : IExpiredDeadlinesCleaner
{
    private readonly IAppDateTime _appDateTime;

    public ExpiredDeadlinesCleaner(IAppDateTime appDateTime)
    {
        _appDateTime = appDateTime;
    }

    public async Task Clean(IDbContext dbContext, CancellationToken cancellationToken)
    {
        await CleanCartItems(dbContext, cancellationToken);
        await CleanDropItemsFromExpiredDrops(dbContext, cancellationToken);
    }

    private async Task CleanCartItems(IDbContext dbContext, CancellationToken cancellationToken)
    {
        var cartItemsWithExpiredReservationTimeAndStatusReserved = await dbContext.CartItems
            .Include(i => i.DropItem)
            .Where(i =>
                i.ReservationEndDateTime < _appDateTime.Now &&
                i.DropItem.Status == DropItemStatus.Reserved)
            .ToListAsync(cancellationToken);

        var cleanedCartItemsCount = 0;
        foreach (var cartItem in cartItemsWithExpiredReservationTimeAndStatusReserved)
        {
            cartItem.DropItem.Status = DropItemStatus.Available;
            cleanedCartItemsCount++;
        }

        Console.WriteLine($"ExpiredDeadlinesCleaner | cleaned cart items count: {cleanedCartItemsCount}");

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task CleanDropItemsFromExpiredDrops(IDbContext dbContext, CancellationToken cancellationToken)
    {
        var notOrderedDropItemsFromExpiredDrops = await dbContext.DropItems
            .Include(d => d.Drop)
            .Include(d => d.Variant)
            .Where(d =>
                d.Drop.EndDateTime < _appDateTime.Now &&
                d.Variant.Status == VariantStatus.Drop &&
                d.Status != DropItemStatus.Ordered)
            .ToListAsync(cancellationToken);

        var cleanedDropItemsCount = 0;
        foreach (var dropItem in notOrderedDropItemsFromExpiredDrops)
        {
            dropItem.Variant.Status = VariantStatus.Warehouse;
            cleanedDropItemsCount++;
        }

        Console.WriteLine($"ExpiredDeadlinesCleaner | cleaned drop items count: {cleanedDropItemsCount}");

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}