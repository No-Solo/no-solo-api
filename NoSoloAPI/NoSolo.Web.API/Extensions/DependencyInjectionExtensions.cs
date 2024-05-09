using System.Diagnostics.CodeAnalysis;

namespace NoSolo.Web.API.Extensions;

[ExcludeFromCodeCoverage]
public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddNoSoloServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransitService();
        
        return services;
    }
}