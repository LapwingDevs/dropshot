namespace DropShot.Application.Common;

public interface ITokenManager
{
    Task<bool> IsCurrentActiveToken();
    Task<bool> IsInCorrectRole();
    Task<bool> IsActive(string token);
    Task DeactivateCurrent();
}