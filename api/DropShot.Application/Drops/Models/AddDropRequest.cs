namespace DropShot.Application.Drops.Models;

public class AddDropRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public ICollection<CreateDropItemDto> DropItems { get; set; }
}