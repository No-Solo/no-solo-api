namespace NoSolo.Core.Exceptions;

public class NotAccessException : BaseException.BaseException
{
    public override int StatusCode { get; protected set; } = 403;

    public NotAccessException(string message) : base(message)
    {
        
    }

    public NotAccessException() : this("You don't have permission")
    {
        
    }
}