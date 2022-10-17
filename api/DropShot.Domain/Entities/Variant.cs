namespace DropShot.Domain.Entities;

public class Variant
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public int Size { get; set; }
    
    public Product Product { get; set; }
    public int ProductId { get; set; }

    public ICollection<DropItem> DropItems { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}