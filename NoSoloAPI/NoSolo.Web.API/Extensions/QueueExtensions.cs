using System.Diagnostics.CodeAnalysis;
using MassTransit;
using NoSolo.Worker;

namespace NoSolo.Web.API.Extensions;

[ExcludeFromCodeCoverage]
public static class QueueExtensions
{
    public static IServiceCollection AddMassTransitService(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.AddEntityFrameworkOutbox<AppDbContext>(o =>
            {
                o.QueryDelay = TimeSpan.FromSeconds(1);
                o.UsePostgres().UseBusOutbox();
            });

            x.UsingAmazonSqs((ctx, cfg) =>
            {
                cfg.Host("eu-west-2", _ => { });
                cfg.ConfigureEndpoints(ctx);
            });
        });

        return services;
    }
}