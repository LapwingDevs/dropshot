using DropShot.Domain.Entities;
using MediatR;

namespace DropShot.Application.Carts.Events;

public class CartItemIsAddedEvent : INotification
{
    public CartItem CartItem { get; }

    public CartItemIsAddedEvent(CartItem cartItem)
    {
        CartItem = cartItem;
    }
}