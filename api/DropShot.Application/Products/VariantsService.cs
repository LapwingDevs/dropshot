using DropShot.Application.Common;
using DropShot.Application.Products.Interfaces;
using DropShot.Application.Products.Models;
using DropShot.Domain.Entities;

namespace DropShot.Application.Products;

public class VariantsService : IVariantsService
{
    private readonly IDbContext _dbContext;

    public VariantsService(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

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
}