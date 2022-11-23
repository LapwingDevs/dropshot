namespace DropShot.Infrastructure.WebSockets;

public interface IDropHubClient
{
    Task DropItemReserved(int dropItemId);
    Task DropItemReleased(int dropItemId);
}