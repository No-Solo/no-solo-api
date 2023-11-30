namespace NoSolo.Core.Exceptions;

public class EmailConfirmException : BaseException.BaseException
{
    public override int StatusCode { get; protected set; } = 402;

    public EmailConfirmException(string message) : base(message)
    {
        
    }
}