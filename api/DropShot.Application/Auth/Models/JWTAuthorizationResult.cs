namespace DropShot.Application.Auth.Models;

public class JWTAuthorizationResult : Result
{
    internal JWTAuthorizationResult(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
    {
    }
    
    public string Token { get; set; }

    public string RefreshToken { get; set; }  
    
    public static JWTAuthorizationResult Success(string accessToken, string refreshToken)
    {
        return new JWTAuthorizationResult(true, new string[] { }) { Token = accessToken, RefreshToken = refreshToken};
    }

    public new static JWTAuthorizationResult Failure(IEnumerable<string> errors)
    {
        return new JWTAuthorizationResult(false, errors);
    }
}