namespace NoSolo.Web.API.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddNoSoloServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransitService();
        
        return services;
    }
}