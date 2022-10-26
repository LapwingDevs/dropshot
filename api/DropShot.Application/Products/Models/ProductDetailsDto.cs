using DropShot.Application.Common.AutoMapper;
using DropShot.Domain.Entities;
using DropShot.Domain.Enums;

namespace DropShot.Application.Products.Models;

public class ProductDetailsDto : IMapFrom<Product>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public ClothesUnitOfMeasure UnitOfSize { get; set; }
    public ICollection<VariantOnProductDto> Variants { get; set; }
}