using DropShot.Application.Common;
using DropShot.Application.Common.Abstraction;
using DropShot.Domain.Entities;
using DropShot.Infrastructure.Identity;
using DropShot.Infrastructure.Identity.Models;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DropShot.Infrastructure.DAL;

public class DropShotDbContext : ApiAuthorizationDbContext<ApplicationUser>, IDbContext
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

    public DropShotDbContext(
        DbContextOptions<DropShotDbContext> options,
        IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
    {
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}