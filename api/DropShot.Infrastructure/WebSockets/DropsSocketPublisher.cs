using DropShot.Application.Common;
using DropShot.Application.Common.Abstraction;
using Microsoft.AspNetCore.SignalR;

namespace DropShot.Infrastructure.WebSockets;

public class DropsSocketPublisher: IDropsSocketPublisher
{
    private readonly IHubContext<DropsHub, IDropHubClient> _dropsHub;
    
    public DropsSocketPublisher(IHubContext<DropsHub, IDropHubClient> dropsHub) => _dropsHub = dropsHub;

    public async Task DropItemIsReserved(int dropId, int dropItemId) => 
        await _dropsHub.Clients.Group(dropId.ToString()).DropItemReserved(dropItemId);
    
    public async Task DropItemIsReleased(int dropId, int dropItemId) => 
        await _dropsHub.Clients.Group(dropId.ToString()).DropItemReleased(dropItemId);
}