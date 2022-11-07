namespace DropShot.Application.Carts.Models;

public class CartItemDto
{
    public string ProductName { get; set; }
    public int VariantSize { get; set; }
    public decimal ProductPrice { get; set; }
    public DateTime ItemReservationEndDateTime { get; set; }
}