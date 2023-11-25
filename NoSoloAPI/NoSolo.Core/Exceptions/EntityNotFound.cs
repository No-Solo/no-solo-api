namespace NoSolo.Core.Exceptions;

public class EntityNotFound : BaseException.BaseException
{
    public override int StatusCode { get; protected set; } = 404;

    public EntityNotFound(string message) : base(message)
    {
        
    }

    public EntityNotFound() : this("The entity is not found")
    {

    }
}