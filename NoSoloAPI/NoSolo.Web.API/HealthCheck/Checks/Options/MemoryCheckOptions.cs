namespace NoSolo.Web.API.HealthCheck.Checks.Options;

public class MemoryCheckOptions
{
    public required string MemoryStatus { get; set; }

    public required long Threshold { get; set; } = 1024L * 1024L * 1024L;
}