namespace DropShot.Domain.Entities;

public class Drop
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }

    public ICollection<DropItem> DropItems { get; set; }
}