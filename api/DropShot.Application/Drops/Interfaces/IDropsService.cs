using DropShot.Application.Drops.Models;

namespace DropShot.Application.Drops.Interfaces;

public interface IDropsService
{
    Task<DropsLandingPageVm> GetDrops();
    Task<DropDetailsDto> GetDropDetails(int dropId);
    Task<IEnumerable<DropDetailsDto>> GetDropsWithDetails();
    
    Task AddDrop(AddDropRequest request);
}