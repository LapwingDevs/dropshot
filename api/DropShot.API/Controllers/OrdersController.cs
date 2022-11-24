using DropShot.Application.Orders.Interfaces;
using DropShot.Application.Orders.Models;
using DropShot.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DropShot.API.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrdersService _ordersService;

    public OrdersController(IOrdersService ordersService)
    {
        _ordersService = ordersService;
    }

    [HttpPost("submit")]
    public async Task SubmitOrder(SubmitOrderRequest request)
    {
        const int userId = 1;
        await _ordersService.SubmitOrder(userId, request);
    }

    [HttpPost("paid")]
    public async Task SetOrderAsPaid(int orderId)
    {
        await _ordersService.SetOrderAsPaid(orderId);
    }
}