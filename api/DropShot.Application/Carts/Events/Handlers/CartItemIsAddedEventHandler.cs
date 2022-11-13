using DropShot.Application.Common;
using MediatR;

namespace DropShot.Application.Carts.Events.Handlers;

public class CartItemIsAddedEventHandler : INotificationHandler<CartItemIsAddedEvent>
{
    private readonly IDeadlinesHandlerAccessor _deadlinesHandlerAccessor;

    public CartItemIsAddedEventHandler(IDeadlinesHandlerAccessor deadlinesHandlerAccessor)
    {
        _deadlinesHandlerAccessor = deadlinesHandlerAccessor;
    }

    public Task Handle(CartItemIsAddedEvent notification, CancellationToken cancellationToken)
    {
        _deadlinesHandlerAccessor.AddCartItemToSchedule(notification.CartItem);
        return Task.CompletedTask;
    }
}