using DropShot.Application.Carts.Interfaces;
using DropShot.Application.Carts.Models;
using DropShot.Application.Common;
using DropShot.Domain.Constants;
using DropShot.Domain.Entities;
using DropShot.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace DropShot.Application.Carts;

public class CartService : ICartService
{
    private readonly IDbContext _dbContext;
    private readonly IAppDateTime _appDateTime;

    public CartService(IDbContext dbContext, IAppDateTime appDateTime)
    {
        _dbContext = dbContext;
        _appDateTime = appDateTime;
    }

    public async Task<UserCartDto> GetUserCartWithItems(int userId)
    {
        var userCart = await GetUserCart(userId);

        var cartItems = await _dbContext.CartItems
            .Include(cartItem => cartItem.DropItem)
            .ThenInclude(dropItem => dropItem.Variant)
            .ThenInclude(variant => variant.Product)
            .Where(cartItem =>
                cartItem.CartId == userCart.Id &&
                cartItem.ReservationEndDateTime > _appDateTime.Now)
            .ToListAsync();

        return new UserCartDto()
        {
            Id = userCart.Id,
            CartItems = cartItems.Select(cartItem => new CartItemDto()
            {
                ProductName = cartItem.DropItem.Variant.Product.Name,
                VariantSize = cartItem.DropItem.Variant.Size,
                ProductPrice = cartItem.DropItem.Variant.Product.Price,
                ItemReservationEndDateTime = cartItem.ReservationEndDateTime
            })
        };
    }


    public async Task AddDropItemToUserCart(AddDropItemToUserCartRequest request)
    {
        await CreateCartItem(request);
        await UpdateStatusOfDropItem(request.DropItemId);

        await _dbContext.SaveChangesAsync(CancellationToken.None);
    }

    private async Task<Cart> GetUserCart(int userId)
    {
        var userCart = await _dbContext.Carts.SingleOrDefaultAsync(c => c.UserId == userId);
        if (userCart is null)
        {
            return await CreateUserCart(userId);
        }

        return userCart;
    }

    private async Task<Cart> CreateUserCart(int userId)
    {
        var cart = new Cart() { UserId = userId };

        await _dbContext.Carts.AddAsync(cart);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        return cart;
    }

    private async Task CreateCartItem(AddDropItemToUserCartRequest request)
    {
        var reservationTime = _appDateTime.Now;
        var cartItem = new CartItem()
        {
            CartId = request.UserCartId,
            DropItemId = request.DropItemId,
            ReservationStartDateTime = reservationTime,
            ReservationEndDateTime = reservationTime.AddMinutes(CartItemReservationTime.TimeInMinutes),
        };

        await _dbContext.CartItems.AddAsync(cartItem);
    }

    private async Task UpdateStatusOfDropItem(int dropItemId)
    {
        var dropItem = await _dbContext.DropItems.FindAsync(dropItemId);
        if (dropItem is null)
        {
            throw new Exception();
        }

        dropItem.Status = DropItemStatus.Ordered;
    }
}