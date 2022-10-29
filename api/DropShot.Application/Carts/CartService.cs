using DropShot.Application.Carts.Interfaces;
using DropShot.Application.Carts.Models;
using DropShot.Application.Common;
using DropShot.Domain.Constants;
using DropShot.Domain.Entities;

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

    public Task<UserCartDto> GetUserCart(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task AddDropItemToUserCart(AddDropItemToUserCartRequest request)
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
        await _dbContext.SaveChangesAsync(CancellationToken.None);
    }
}