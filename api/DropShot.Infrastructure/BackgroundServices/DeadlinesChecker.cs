using DropShot.Application.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DropShot.Infrastructure.BackgroundServices;

// TODO: Updating schedule when sb add cart item or new drop is added.
public class DeadlinesChecker : BackgroundService
{
    private const int DelayBetweenChecks = 1000;
    
    private readonly IServiceProvider _serviceProvider;
    private readonly IAppDateTime _appDateTime;
    
    private List<ScheduleItem> _schedule = new();


    public DeadlinesChecker(IServiceProvider serviceProvider, IAppDateTime appDateTime)
    {
        _serviceProvider = serviceProvider;
        _appDateTime = appDateTime;
    }

    private async Task InitSchedule(CancellationToken cancellationToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<IDbContext>();
            var drops = await dbContext.Drops.ToListAsync(cancellationToken);
            var cartItems = await dbContext.CartItems.ToListAsync(cancellationToken);

            var dropsSchedule =
                drops.Select(d => new ScheduleItem(d.EndDateTime, ScheduleItemType.Drop, d.Id));
            var cartItemsSchedule =
                cartItems.Select(i => new ScheduleItem(i.ReservationEndDateTime, ScheduleItemType.Drop, i.Id));

            _schedule.AddRange(dropsSchedule);
            _schedule.AddRange(cartItemsSchedule);
            _schedule = _schedule.OrderBy(s => s.ExecuteTime).ToList();
        }
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            await InitSchedule(cancellationToken);
            await base.StartAsync(cancellationToken);
            Console.WriteLine("Hello there - I started my work");
            // TODO: If there is something expired - cleanup. Rest of the data convert into 'schedule'
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
                await Task.Delay(DelayBetweenChecks, stoppingToken);
                Console.WriteLine($"TEST {_appDateTime.Now}");

                if (!_schedule.Any())
                {
                    continue;
                }

                if (_schedule[0].ExecuteTime == _appDateTime.Now)
                {
                    // TODO: DO WORK && REMOVE FROM SCHEDULE
                }
            }
            catch (Exception e)
            {
                return;
            }
        }
    }
}