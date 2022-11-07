using DropShot.Application.Common.AutoMapper;
using DropShot.Domain.Entities;

namespace DropShot.Application.Carts.Models;

public class UserCartDto
{
    public int Id { get; set; }
    public IEnumerable<CartItemDto> CartItems { get; set; }
}