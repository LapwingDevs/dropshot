namespace DropShot.API.Middleware;

public interface ITokenManager
{
    Task<bool> IsCurrentActiveToken();
    
    Task<bool> IsActive(string token);
    Task DeactivateCurrent();
}