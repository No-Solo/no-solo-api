namespace NoSolo.Core.Exceptions;

public class BadRequestException : BaseException.BaseException
{
    public override int StatusCode { get; protected set; } = 400;

    public BadRequestException(string message) : base(message)
    {

    }

    public BadRequestException() : this("Throw bad request")
    {
        
    }
}