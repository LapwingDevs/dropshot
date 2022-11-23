using DropShot.Application.Common;
using MediatR;

namespace DropShot.Application.Carts.Events.Handlers;

public class CartItemIsAddedEventHandler : INotificationHandler<CartItemIsAddedEvent>
{
    private readonly IDeadlinesHandlerAccessor _deadlinesHandlerAccessor;
    private readonly IDropsSocketPublisher _dropsSocketPublisher;

    public CartItemIsAddedEventHandler(
        IDeadlinesHandlerAccessor deadlinesHandlerAccessor,
        IDropsSocketPublisher dropsSocketPublisher)
    {
        _deadlinesHandlerAccessor = deadlinesHandlerAccessor;
        _dropsSocketPublisher = dropsSocketPublisher;
    }

    public async Task Handle(CartItemIsAddedEvent notification, CancellationToken cancellationToken)
    {
        _deadlinesHandlerAccessor.AddCartItemToSchedule(notification.CartItem);
        
        await _dropsSocketPublisher.DropItemIsReserved(
            notification.CartItem.DropItem.DropId,
            notification.CartItem.DropItemId);
    }
}