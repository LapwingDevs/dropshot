using DropShot.Application.Drops.Models;

namespace DropShot.Application.Drops.Interfaces;

public interface IDropsListConverter
{
    DropsLandingPageVm ConvertDropsListToLandingPageVm(IEnumerable<DropCardDto> drops);
}