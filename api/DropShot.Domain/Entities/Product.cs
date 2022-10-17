using DropShot.Domain.Enums;

namespace DropShot.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ClothesUnitOfMeasure UnitOfSize { get; set; }

    public ICollection<Variant> Variants { get; set; }
}