namespace DropShot.Application.Auth.Models;

public class JWTAuthorizationResult : Result
{
    internal JWTAuthorizationResult(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
    {
    }
    
    public string Token { get; set; }
    
    public static JWTAuthorizationResult Success(string token)
    {
        return new JWTAuthorizationResult(true, new string[] { }) { Token = token };
    }

    public new static JWTAuthorizationResult Failure(IEnumerable<string> errors)
    {
        return new JWTAuthorizationResult(false, errors);
    }
}