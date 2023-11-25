namespace NoSolo.Core.Exceptions;

public class PhotoException : BaseException.BaseException
{
    public override int StatusCode { get; protected set; } = 400;

    public PhotoException(string message) : base(message)
    {
        
    }
}