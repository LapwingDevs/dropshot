namespace DropShot.Application.Common.Abstraction;

public interface IDropsSocketPublisher
{
    Task DropItemIsReserved(int dropId, int dropItemId);
    Task DropItemIsReleased(int dropId, int dropItemId);
}