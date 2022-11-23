using Microsoft.AspNetCore.SignalR;

namespace DropShot.Infrastructure.WebSockets;

public class DropsHub : Hub<IDropHubClient>
{
    public async Task JoinDropHub(int dropId) => 
        await Groups.AddToGroupAsync(Context.ConnectionId, dropId.ToString());
}