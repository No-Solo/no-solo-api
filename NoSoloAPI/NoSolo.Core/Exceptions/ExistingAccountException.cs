namespace NoSolo.Core.Exceptions;

public class ExistingAccountException : BaseException.BaseException
{
    public override int StatusCode { get; protected set; } = 402;

    public ExistingAccountException(string message) : base(message)
    {
        
    }

    public ExistingAccountException() : this("Account with this email already exists")
    {
        
    }
}