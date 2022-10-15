namespace DropShot.Domain.Entities;

public class OrderItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public Order Order { get; set; }
    public int OrderId { get; set; }

    public Variant Variant { get; set; }
    public int VariantId { get; set; }
}