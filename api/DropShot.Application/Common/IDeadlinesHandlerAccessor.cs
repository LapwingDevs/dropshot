using DropShot.Domain.Entities;

namespace DropShot.Application.Common;

public interface IDeadlinesHandlerAccessor
{
    void AddDropToSchedule(Drop drop);
    void AddCartItemToSchedule(CartItem cartItem);
}