using DropShot.Domain.Entities;
using MediatR;

namespace DropShot.Application.Drops.Events;

public class DropIsCreatedEvent : INotification
{
    public Drop Drop { get; }

    public DropIsCreatedEvent(Drop drop)
    {
        Drop = drop;
    }
}