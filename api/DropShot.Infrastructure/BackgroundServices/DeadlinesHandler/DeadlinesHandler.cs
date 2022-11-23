using DropShot.Application.Common;
using DropShot.Domain.Entities;
using DropShot.Domain.Enums;
using DropShot.Infrastructure.AppDateTime;
using DropShot.Infrastructure.BackgroundServices.DeadlinesHandler.ExpiredDeadlinesCleaner;
using DropShot.Infrastructure.BackgroundServices.DeadlinesHandler.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DropShot.Infrastructure.BackgroundServices.DeadlinesHandler;

internal class DeadlinesHandler : BackgroundService
{
    private const int DelayBetweenChecksInMilliseconds = 1000;

    private readonly IServiceProvider _serviceProvider;
    private readonly IAppDateTime _appDateTime;
    private readonly IExpiredDeadlinesCleaner _expiredDeadlinesCleaner;
    private readonly IDropsSocketPublisher _dropsSocketPublisher;

    private List<ScheduleItem> _schedule = new();


    public DeadlinesHandler(
        IServiceProvider serviceProvider,
        IAppDateTime appDateTime,
        IExpiredDeadlinesCleaner expiredDeadlinesCleaner,
        IDropsSocketPublisher dropsSocketPublisher)
    {
        _serviceProvider = serviceProvider;
        _appDateTime = appDateTime;
        _expiredDeadlinesCleaner = expiredDeadlinesCleaner;
        _dropsSocketPublisher = dropsSocketPublisher;
    }


    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            await CleanExpiredDeadlines(cancellationToken);
            await InitSchedule(cancellationToken);
            await base.StartAsync(cancellationToken);
            Console.WriteLine("Hello there - I started my work");
        }
        catch (Exception e)
        {
            await StopAsync(cancellationToken);
            Console.WriteLine("Background worker does not work");
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(DelayBetweenChecksInMilliseconds, stoppingToken);

                if (!_schedule.Any())
                {
                    Console.WriteLine($"SCHEDULE CHECK: {_appDateTime.Now} | schedule is empty ");
                    continue;
                }

                Console.WriteLine(
                    $"SCHEDULE CHECK: {_appDateTime.Now} | schedule length {_schedule.Count} |next schedule item: {_schedule[0].ExecuteTime}");

                if (_schedule[0].ExecuteTime != _appDateTime.Now.TrimMilliseconds())
                {
                    continue;
                }

                Console.WriteLine("will handle!");
                await HandleScheduleItem(stoppingToken);
            }
            catch (Exception e)
            {
                return;
            }
        }
    }

    public void AddScheduleItem(ScheduleItem scheduleItem)
    {
        _schedule.Add(scheduleItem);
        _schedule = _schedule.OrderBy(s => s.ExecuteTime).ToList();
    }

    private async Task CleanExpiredDeadlines(CancellationToken cancellationToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<IDbContext>();
            await _expiredDeadlinesCleaner.Clean(dbContext, cancellationToken);
        }
    }

    private async Task InitSchedule(CancellationToken cancellationToken)
    {
        var drops = await GetActiveAndIncomingDropsFromDatabase(cancellationToken);
        var cartItems = await GetActiveCartItemsFromDatabase(cancellationToken);

        var dropsSchedule = drops
            .Select(d =>
                new ScheduleItem(d.EndDateTime.TrimMilliseconds(), ScheduleItemType.Drop, d.Id));
        var cartItemsSchedule =
            cartItems.Select(i =>
                new ScheduleItem(i.ReservationEndDateTime.TrimMilliseconds(), ScheduleItemType.CartItem, i.Id));

        _schedule.AddRange(dropsSchedule);
        _schedule.AddRange(cartItemsSchedule);
        _schedule = _schedule.OrderBy(s => s.ExecuteTime).ToList();
    }

    private async Task HandleScheduleItem(CancellationToken stoppingToken)
    {
        switch (_schedule[0].ScheduleItemType)
        {
            case ScheduleItemType.Drop:
                Console.WriteLine("drop");
                await MoveNotBoughtDropItemStatusAsWarehouse(_schedule[0].Id, stoppingToken);
                break;
            case ScheduleItemType.CartItem:
                Console.WriteLine("cart item");
                await MoveCartItemBackToDrop(_schedule[0].Id, stoppingToken);
                break;
            default:
                Console.WriteLine("wrong item type");
                break;
        }

        _schedule.RemoveAt(0);
    }

    private async Task MoveCartItemBackToDrop(int cartItemId, CancellationToken cancellationToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<IDbContext>();
            var cartItem = await dbContext.CartItems
                .Include(i => i.DropItem)
                .SingleOrDefaultAsync(i => i.Id == cartItemId, cancellationToken);
            if (cartItem is null)
            {
                return;
            }

            if (cartItem.DropItem.Status == DropItemStatus.Ordered)
            {
                return;
            }

            cartItem.DropItem.Status = DropItemStatus.Available;

            await dbContext.SaveChangesAsync(cancellationToken);
            
            await _dropsSocketPublisher.DropItemIsReleased(cartItem.DropItem.DropId, cartItem.DropItemId);
        }
    }

    private async Task MoveNotBoughtDropItemStatusAsWarehouse(int dropId, CancellationToken cancellationToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<IDbContext>();
            var drop = await dbContext.Drops
                .Include(d => d.DropItems)
                .SingleOrDefaultAsync(d => d.Id == dropId, cancellationToken);

            if (drop is null)
            {
                return;
            }

            var variantsIncluded = drop.DropItems.Select(x => x.VariantId);
            var variants = await dbContext.Variants
                .Where(v => variantsIncluded.Contains(v.Id))
                .ToListAsync(cancellationToken);


            foreach (var dropItems in drop.DropItems)
            {
                if (dropItems.Status != DropItemStatus.Available)
                {
                    continue;
                }

                var variant = variants.SingleOrDefault(v => v.Id == dropItems.VariantId);
                if (variant is null)
                {
                    continue;
                }

                variant.Status = VariantStatus.Warehouse;
            }

            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    private async Task<IEnumerable<Drop>> GetActiveAndIncomingDropsFromDatabase(CancellationToken cancellationToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<IDbContext>();
            var drops = await dbContext.Drops
                .Include(d => d.DropItems)
                .Where(d => d.EndDateTime > _appDateTime.Now)
                .ToListAsync(cancellationToken);
            return drops;
        }
    }

    private async Task<IEnumerable<CartItem>> GetActiveCartItemsFromDatabase(CancellationToken cancellationToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<IDbContext>();
            var cartItems = await dbContext.CartItems
                .Where(d => d.ReservationEndDateTime > _appDateTime.Now)
                .ToListAsync(cancellationToken);
            return cartItems;
        }
    }
}