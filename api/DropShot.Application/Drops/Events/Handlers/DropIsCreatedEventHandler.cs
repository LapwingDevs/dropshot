using DropShot.Application.Common;
using DropShot.Application.Common.Abstraction;
using MediatR;

namespace DropShot.Application.Drops.Events.Handlers;

public class DropIsCreatedEventHandler : INotificationHandler<DropIsCreatedEvent>
{
    private readonly IDeadlinesHandlerAccessor _deadlinesHandlerAccessor;

    public DropIsCreatedEventHandler(IDeadlinesHandlerAccessor deadlinesHandlerAccessor)
    {
        _deadlinesHandlerAccessor = deadlinesHandlerAccessor;
    }

    public Task Handle(DropIsCreatedEvent notification, CancellationToken cancellationToken)
    {
        _deadlinesHandlerAccessor.AddDropToSchedule(notification.Drop);
        return Task.CompletedTask;
    }
}