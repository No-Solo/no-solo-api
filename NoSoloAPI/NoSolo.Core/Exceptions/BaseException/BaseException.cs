namespace NoSolo.Core.Exceptions.BaseException;

public abstract class BaseException : Exception
{
    public abstract int StatusCode { get; protected set; }
    
    public BaseException()
    {
        
    }

    public BaseException(string message) : base(message)
    {
        
    }
}