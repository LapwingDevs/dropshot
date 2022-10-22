namespace DropShot.Application.Models.Drops;

public class DropsVm
{
    public IReadOnlyCollection<DropCardDto> ActiveDrops { get; set; }
    public IReadOnlyCollection<DropCardDto> IncomingDrops { get; set; }
}