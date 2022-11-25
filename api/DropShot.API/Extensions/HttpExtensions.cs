namespace DropShot.API.Extensions;

public static class HttpExtensions
{
    public static HttpResponse AddRefreshTokenCookie(this HttpResponse response, string cookie)
    {
        response.Cookies.Append("X-Refresh-Token", cookie, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.None, Secure = true });
        
        return response;
    }
}