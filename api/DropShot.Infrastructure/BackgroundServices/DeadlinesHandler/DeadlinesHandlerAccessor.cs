using DropShot.Application.Common;
using DropShot.Domain.Entities;
using DropShot.Infrastructure.AppDateTime;
using DropShot.Infrastructure.BackgroundServices.DeadlinesHandler.Extensions;
using DropShot.Infrastructure.BackgroundServices.DeadlinesHandler.Models;

namespace DropShot.Infrastructure.BackgroundServices.DeadlinesHandler;

public class DeadlinesHandlerAccessor : IDeadlinesHandlerAccessor
{
    private readonly DeadlinesHandler _deadlinesHandler;

    public DeadlinesHandlerAccessor(IServiceProvider serviceProvider)
    {
        _deadlinesHandler = serviceProvider.GetHostedService<DeadlinesHandler>();
    }

    public void AddDropToSchedule(Drop drop) =>
        _deadlinesHandler.AddScheduleItem(
            new ScheduleItem(
                drop.EndDateTime.TrimMilliseconds(),
                ScheduleItemType.Drop,
                drop.Id));

    public void AddCartItemToSchedule(CartItem cartItem) =>
        _deadlinesHandler.AddScheduleItem(
            new ScheduleItem(
                cartItem.ReservationEndDateTime.TrimMilliseconds(),
                ScheduleItemType.CartItem,
                cartItem.Id));
}