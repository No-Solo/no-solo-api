using Microsoft.AspNetCore.Mvc;
using NoSolo.Web.API.Errors;

namespace NoSolo.Web.API.Extensions;

public static class ApiBehaviorServiceExtensions
{
    public static IServiceCollection AddApiBehaviourServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                policy => { policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
        });
        
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var errors = context.ModelState
                    .Where(error => error.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage).ToArray();

                var errorResponse = new ApiValidationErrorResponse
                {
                    Errors = errors
                };

                return new BadRequestObjectResult(errorResponse);
            };
        });

        return services;
    }
}