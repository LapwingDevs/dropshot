using DropShot.Application.Common.AutoMapper;
using DropShot.Domain.Entities;

namespace DropShot.Application.Carts.Models;

public class UserCartDto : IMapFrom<Cart>
{
    public int Id { get; set; }
}