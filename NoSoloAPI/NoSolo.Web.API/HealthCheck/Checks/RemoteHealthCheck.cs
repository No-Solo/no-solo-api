using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace NoSolo.Web.API.HealthCheck.Checks;

[ExcludeFromCodeCoverage]
public class RemoteHealthCheck(IHttpClientFactory httpClientFactory) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
        CancellationToken cancellationToken = new())
    {
        using var httpClient = httpClientFactory.CreateClient();
        var response = await httpClient.GetAsync("https://localhost:5001/api/feed-back", cancellationToken);
        return response.IsSuccessStatusCode
            ? HealthCheckResult.Healthy($"Remote endpoints is healthy.")
            : HealthCheckResult.Unhealthy("Remote endpoint is unhealthy");
    }
}