using DropShot.Application.Models.Drops;

namespace DropShot.Application.Interfaces;

public interface IDropsService
{
    Task<DropsVm> GetDrops();
    Task AddDrop(AddDropRequest request);
}