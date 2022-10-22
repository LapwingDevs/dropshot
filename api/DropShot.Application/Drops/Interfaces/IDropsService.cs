using DropShot.Application.Drops.Models;

namespace DropShot.Application.Drops.Interfaces;

public interface IDropsService
{
    Task<DropsLandingPageVm> GetDrops();
    Task<DropDetailsDto> GetDropDetails(int dropId);
    Task AddDrop(AddDropRequest request);
}