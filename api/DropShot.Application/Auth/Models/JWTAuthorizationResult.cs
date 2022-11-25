namespace DropShot.Application.Auth.Models;

public class JWTAuthorizationResult : Result
{
    internal JWTAuthorizationResult(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
    {
    }
    
    public string Token { get; set; }

    public static JWTAuthorizationResult Success(string accessToken)
    {
        return new JWTAuthorizationResult(true, new string[] { }) { Token = accessToken};
    }

    public new static JWTAuthorizationResult Failure(IEnumerable<string> errors)
    {
        return new JWTAuthorizationResult(false, errors);
    }
}