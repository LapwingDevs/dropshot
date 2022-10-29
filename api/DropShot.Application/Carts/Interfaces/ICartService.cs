using DropShot.Application.Carts.Models;

namespace DropShot.Application.Carts.Interfaces;

public interface ICartService
{
    Task<UserCartDto> GetUserCart(int userId);

    Task AddDropItemToUserCart(AddDropItemToUserCartRequest request);
}