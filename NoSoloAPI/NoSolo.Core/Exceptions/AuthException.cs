namespace NoSolo.Core.Exceptions;

public class AuthException : BaseException.BaseException
{
    public override int StatusCode { get; protected set; } = 401;

    public AuthException(string message) : base(message)
    {
        
    }
}