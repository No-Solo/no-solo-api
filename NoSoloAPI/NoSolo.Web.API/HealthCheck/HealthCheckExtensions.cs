using HealthChecks.UI.Client;
using HealthChecks.UI.Configuration;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using NoSolo.Web.API.HealthCheck.Checks;

namespace NoSolo.Web.API.HealthCheck;

public static class HealthCheckExtensions
{
    private static readonly string[] Tags = { "Feedback", "Database" };

    public static IServiceCollection ConfigureHealthChecks(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddNpgSql(configuration["ConnectionStrings:DefaultConnectionString"]!, healthQuery: "Select 1", name: "PostgreSQL main",
                failureStatus: HealthStatus.Unhealthy, tags: Tags)
            .AddNpgSql(configuration["ConnectionStrings:FeedBackConnectionString"]!, healthQuery: "Select 1", name: "PostgreSQL main",
                failureStatus: HealthStatus.Unhealthy, tags: Tags)
            .AddCheck<RemoteHealthCheck>("Remote endpoints Health Check", failureStatus: HealthStatus.Unhealthy)
            .AddCheck<MemoryHealthCheck>($"Feedback Service Memory Check", failureStatus: HealthStatus.Unhealthy,
                tags: new[] { "Feedback Service" });
        
        services.AddHealthChecksUI(opt =>
            {
                opt.SetEvaluationTimeInSeconds(10); //time in seconds between check    
                opt.MaximumHistoryEntriesPerEndpoint(60); //maximum history of checks    
                opt.SetApiMaxActiveRequests(1); //api requests concurrency    
                opt.AddHealthCheckEndpoint("feedback api", "/api/health"); //map health check api    
            })
            .AddInMemoryStorage();

        return services;
    }

    public static WebApplication AddHealthCheck(this WebApplication appBuilder)
    {
        appBuilder.MapHealthChecks("/api/health", new HealthCheckOptions()
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
        appBuilder.UseHealthChecksUI(delegate(Options options) { options.UIPath = "/healthcheck-ui"; });

        return appBuilder;
    }
}