using DropShot.Application.Common;
using DropShot.Application.Drops.Interfaces;
using DropShot.Application.Drops.Models;

namespace DropShot.Application.Drops;

public class DropsListConverter : IDropsListConverter
{
    private readonly IAppDateTime _dateTime;

    public DropsListConverter(IAppDateTime dateTime)
    {
        _dateTime = dateTime;
    }

    public DropsLandingPageVm ConvertDropsListToLandingPageVm(IEnumerable<DropCardDto> drops)
    {
        var activeDrops = new List<DropCardDto>();
        var incomingDrops = new List<DropCardDto>();

        foreach (var drop in drops)
        {
            if (drop.EndDateTime < _dateTime.Now)
            {
                continue;
            }

            if (drop.StartDateTime > _dateTime.Now)
            {
                incomingDrops.Add(drop);
            }
            else
            {
                activeDrops.Add(drop);
            }
        }

        return new DropsLandingPageVm()
        {
            ActiveDrops = activeDrops,
            IncomingDrops = incomingDrops
        };
    }
}