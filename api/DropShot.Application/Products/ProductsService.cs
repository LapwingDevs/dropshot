using AutoMapper;
using AutoMapper.QueryableExtensions;
using DropShot.Application.Common;
using DropShot.Application.Products.Interfaces;
using DropShot.Application.Products.Models;
using DropShot.Domain.Entities;
using DropShot.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace DropShot.Application.Products;

public class ProductsService : IProductsService
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public ProductsService(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductOnListDto>> GetProducts() =>
        await _dbContext.Products
            .ProjectTo<ProductOnListDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

    public async Task<ProductDetailsDto> GetProductById(int productId)
    {
        var product = await _dbContext.Products
            .Include(p => p.Variants)
            .SingleOrDefaultAsync(x => x.Id == productId);

        if (product is null)
        {
            throw new Exception($"Cannot find product with id {productId}");
        }

        product.Variants = product.Variants
            .Where(v => v.Status == VariantStatus.Warehouse)
            .OrderBy(x => x.Size)
            .ToList();

        return _mapper.Map<ProductDetailsDto>(product);
    }

    public async Task AddProduct(AddProductRequest request)
    {
        await _dbContext.Products.AddAsync(new Product()
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            UnitOfSize = request.UnitOfSize
        });

        await _dbContext.SaveChangesAsync(CancellationToken.None);
    }
}