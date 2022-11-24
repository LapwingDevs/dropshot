using DropShot.Application.Common;
using DropShot.Application.Common.Abstraction;
using DropShot.Application.Products.Interfaces;
using DropShot.Application.Products.Models;
using DropShot.Domain.Entities;
using DropShot.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace DropShot.Application.Products;

public class VariantsService : IVariantsService
{
    private readonly IDbContext _dbContext;

    public VariantsService(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<VariantDto>> GetAllVariantsInWarehouse() =>
        await _dbContext.Variants
            .Include(v => v.Product)
            .Where(v => v.Status == VariantStatus.Warehouse)
            .OrderBy(v => v.Product.Name)
            .Select(v => new VariantDto()
            {
                VariantId = v.Id,
                ProductId = v.Product.Id,
                ProductName = v.Product.Name,
                UnitOfSize = v.Product.UnitOfSize,
                Size = v.Size
            }).ToListAsync();

    public async Task AddVariantToProduct(AddVariantToProductRequest request)
    {
        if (request.ProductId < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(request.ProductId));
        }

        // if product has letter sizing validate if it match scope

        var variant = new Variant()
        {
            Size = request.Size,
            ProductId = request.ProductId
        };

        await _dbContext.Variants.AddAsync(variant);
        await _dbContext.SaveChangesAsync(CancellationToken.None);
    }

    public async Task RemoveVariant(int variantId)
    {
        var variant = await _dbContext.Variants.FindAsync(variantId);
        if (variant is null)
        {
            throw new Exception($"Cannot find product with id {variantId}");
        }

        _dbContext.Variants.Remove(variant);
        await _dbContext.SaveChangesAsync(CancellationToken.None);
    }
}