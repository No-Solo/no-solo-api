namespace Core.Entities.Auth;

public class AuthenticationResult
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }

    // Expiry is there to make the client easier to know whether the access token has expired or not. The same info is in AccessToken.
    
    // Expire date time for AccessToken.
    public DateTime Expiry { get; set; }
}