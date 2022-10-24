using DropShot.Application.Common;
using DropShot.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DropShot.Infrastructure.DAL;

public class DropShotDbContext : DbContext, IDbContext
{
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Drop> Drops { get; set; }
    public DbSet<DropItem> DropItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Variant> Variants { get; set; }

    public DropShotDbContext(DbContextOptions<DropShotDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
}