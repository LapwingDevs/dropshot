namespace DropShot.Application.Drops.Models;

public class DropsLandingPageVm
{
    public IReadOnlyCollection<DropCardDto> ActiveDrops { get; set; }
    public IReadOnlyCollection<DropCardDto> IncomingDrops { get; set; }
}