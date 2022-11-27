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
    public async Task<ActionResult<int>> SubmitOrder(SubmitOrderRequest request)
    {
        try
        {
            const int userId = 1;
            var orderId = await _ordersService.SubmitOrder(userId, request);
        
            return Ok(orderId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    [HttpPost("paid/{orderId}")]
    public async Task<ActionResult> SetOrderAsPaid(int orderId)
    {
        await _ordersService.SetOrderAsPaid(orderId);

        return Ok();
    }
}