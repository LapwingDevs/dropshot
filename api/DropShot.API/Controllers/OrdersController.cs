using DropShot.Application.Common.Abstraction;
using DropShot.Application.Orders.Interfaces;
using DropShot.Application.Orders.Models;
using DropShot.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DropShot.API.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IOrdersService _ordersService;

    public OrdersController(IOrdersService ordersService, ICurrentUserService currentUserService)
    {
        _ordersService = ordersService;
        _currentUserService = currentUserService;
    }

    [HttpPost("submit")]
    [Authorize]
    public async Task SubmitOrder(SubmitOrderRequest request)
    {
        await _ordersService.SubmitOrder(_currentUserService.UserId, request);
    }

    [HttpPost("paid")]
    [Authorize]
    public async Task SetOrderAsPaid(int orderId)
    {
        await _ordersService.SetOrderAsPaid(orderId);
    }
}