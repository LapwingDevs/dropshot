using DropShot.Application.Carts.Models;

namespace DropShot.Application.Carts.Interfaces;

public interface ICartService
{
    Task<UserCartDto> GetUserCartWithItems(string applicationUserId);

    Task AddDropItemToUserCart(AddDropItemToUserCartRequest request);
}