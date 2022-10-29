namespace DropShot.API.Middleware;

public interface ITokenManager
{
    Task<bool> IsCurrentActiveToken();
    
    Task<bool> IsActiveAsync(string token);
}