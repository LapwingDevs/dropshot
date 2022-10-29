namespace DropShot.Application.Carts.Models;

public class AddDropItemToUserCartRequest
{
    public int UserCartId { get; set; }
    public int DropItemId { get; set; }
}