namespace DropShot.Application.Common.Abstraction;

public interface ITokenManager
{
    Task<bool> IsCurrentActiveToken();
    Task<bool> IsInCorrectRole();
    Task<bool> IsActive(string token);
    Task DeactivateCurrent();
}