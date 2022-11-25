using DropShot.Application.Carts.Interfaces;
using DropShot.Application.Carts.Models;
using DropShot.Application.Common.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DropShot.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CartsController : ControllerBase
{
    private readonly ICartService _cartService;
    private readonly ICurrentUserService _currentUserService;

    public CartsController(ICartService cartService, ICurrentUserService currentUserService)
    {
        _cartService = cartService;
        _currentUserService = currentUserService;
    }

    [HttpGet]
    [Authorize]
    public Task<UserCartDto> GetUserCart()
    {
        return _cartService.GetUserCartWithItems(_currentUserService.UserId);
    }

    [HttpPost]
    [Authorize]
    public async Task AddDropItemToUserCart(AddDropItemToUserCartRequest request)
    {
        await _cartService.AddDropItemToUserCart(request);
    }
}