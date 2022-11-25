namespace DropShot.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal ShippingCost { get; set; }
    public bool IsPaid { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; }

    public User User { get; set; }
    public int UserId { get; set; }
}