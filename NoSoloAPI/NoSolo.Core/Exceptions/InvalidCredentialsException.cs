namespace NoSolo.Core.Exceptions;

public class InvalidCredentialsException : BaseException.BaseException
{
    public override int StatusCode { get; protected set; } = 403;

    public InvalidCredentialsException(string message) : base(message)
    {
        
    }
}