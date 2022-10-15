namespace DropShot.Domain.Entities;

public class Cart
{
    public int Id { get; set; }

    public User User { get; set; }
    public int UserId { get; set; }

    public ICollection<CartItem> CartItems { get; set; }
    
}