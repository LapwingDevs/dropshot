namespace DropShot.Domain.Entities;

public class CartItem
{
    public int Id { get; set; }
    // public int Quantity { get; set; }
    public DateTime ReservationStartDateTime { get; set; }
    public DateTime ReservationEndDateTime { get; set; }
    
    public DropItem DropItem { get; set; }
    public int DropItemId { get; set; }

    public Cart Cart { get; set; }
    public int CartId { get; set; }
}