using DropShot.Domain.Entities;

namespace DropShot.Application.Common.Abstraction;

public interface IDeadlinesHandlerAccessor
{
    void AddDropToSchedule(Drop drop);
    void AddCartItemToSchedule(CartItem cartItem);
}