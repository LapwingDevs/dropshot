using DropShot.Application.Carts.Interfaces;
using DropShot.Application.Carts.Models;
using Microsoft.AspNetCore.Mvc;

namespace DropShot.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CartsController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartsController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet("user/{userId}")]
    public Task<UserCartDto> GetUserCart(int userId)
    {
        return _cartService.GetUserCartWithItems(userId);
    }

    [HttpPost]
    public async Task AddDropItemToUserCart(AddDropItemToUserCartRequest request)
    {
        await _cartService.AddDropItemToUserCart(request);
    }
}