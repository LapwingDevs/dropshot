using DropShot.Application.Carts.Models;

namespace DropShot.Application.Orders.Models;

public class SubmitOrderRequest
{
    public double TotalPrice { get; set; }
    public double ShippingCost { get; set; }
    public IEnumerable<CartItemDto> CartItems { get; set; }
}