using DropShot.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DropShot.Application.Common;

public interface IDbContext
{
    DbSet<Address> Addresses { get; set; }
    DbSet<Cart> Carts { get; set; }
    DbSet<CartItem> CartItems { get; set; }
    DbSet<Drop> Drops { get; set; }
    DbSet<DropItem> DropItems { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<OrderItem> OrderItems { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<Variant> Variants { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}