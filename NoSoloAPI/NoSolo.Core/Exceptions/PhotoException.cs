namespace NoSolo.Core.Exceptions;

public class PhotoException : BaseException.BaseException
{
    public sealed override int StatusCode { get; protected set; } = 400;

    public PhotoException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
    
    public PhotoException(string message) : base(message)
    {
        
    }
}