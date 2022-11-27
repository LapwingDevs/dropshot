using DropShot.Domain.Enums;

namespace DropShot.Domain.Entities;

public class DropItem
{
    public int Id { get; set; }

    public DropItemStatus Status { get; set; }

    public Drop Drop { get; set; }
    public int DropId { get; set; }

    public Variant Variant { get; set; }
    public int VariantId { get; set; }
    
    public ICollection<CartItem> CartItems { get; set; }
}