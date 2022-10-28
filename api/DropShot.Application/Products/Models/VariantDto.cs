using DropShot.Domain.Enums;

namespace DropShot.Application.Products.Models;

public class VariantDto
{
    public int VariantId { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public ClothesUnitOfMeasure UnitOfSize { get; set; }
    public int Size { get; set; }
}