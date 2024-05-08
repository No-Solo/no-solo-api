using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NoSolo.Worker.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddWorkerService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSqs();
        
        services.AddDatabase(configuration);
        
        return services;
    }

    public static IServiceCollection AddSqs(this IServiceCollection services)
    {
        
        services.AddMassTransit(config =>
        {
            config.AddEntityFrameworkOutbox<AppDbContext>(o =>
            {
                o.DuplicateDetectionWindow = TimeSpan.FromSeconds(30);
                o.UsePostgres();
            });
    
            config.SetKebabCaseEndpointNameFormatter();

            var assembly = typeof(Program).Assembly;
    
            config.AddConsumers(assembly);
            config.AddActivities(assembly);
    
            config.UsingAmazonSqs((ctx, cfg) =>
            {
                cfg.Host("eu-west-2", _ => {});
                cfg.ConfigureEndpoints(ctx);
            });
        });
        
        return services;
    }
    
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseNpgsql(configuration["Database:DefaultConnection"]!, builder =>
            {
                builder.EnableRetryOnFailure(5);
            });
        });

        return services;
    }
}