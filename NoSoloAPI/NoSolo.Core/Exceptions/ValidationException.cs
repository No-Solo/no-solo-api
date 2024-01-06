using System.Runtime.Serialization;
using Microsoft.AspNetCore.Http;

namespace NoSolo.Core.Exceptions;

public class ValidationException : ApplicationException
{
    public string ErrorCode { get; set; }
    public ValidationException() { }

    public ValidationException(string message) : base(message) { }
    public ValidationException(string message, string errorCode) : base(message) { ErrorCode = errorCode; }

    public ValidationException(string message, Exception innerException) : base(message, innerException) { }

    protected ValidationException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    public virtual int HttpStatusCode => StatusCodes.Status422UnprocessableEntity;
}