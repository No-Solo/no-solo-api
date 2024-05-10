using System.Net;
using System.Text.Json;
using NoSolo.Core.Exceptions.BaseException;
using NoSolo.Web.API.Errors;

namespace NoSolo.Web.API.Middleware;

public class ExceptionMiddleware(
    RequestDelegate requestDelegate,
    ILogger<ExceptionMiddleware> logger,
    IHostEnvironment environment)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await requestDelegate(context);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, exception.Message);

            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new ApiResponse((int)HttpStatusCode.InternalServerError);

        if (environment.IsDevelopment() && exception is BaseException customException &&
            customException.StatusCode != 0)
        {
            response = new ApiException(customException.StatusCode, customException.Message,
                customException.StackTrace);
            context.Response.StatusCode = customException.StatusCode;
        }

        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        var json = JsonSerializer.Serialize(response, options);
        await context.Response.WriteAsync(json);
    }
}