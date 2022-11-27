using DropShot.Application.Orders.Models;

namespace DropShot.Application.Orders.Interfaces;

public interface IOrdersService
{
    Task<int> SubmitOrder(int userId, SubmitOrderRequest request);
    Task SetOrderAsPaid(int orderId);
}