using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics;

public class SystemHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var process = Process.GetCurrentProcess();

        // Get CPU usage
        var cpuUsage = process.TotalProcessorTime.TotalSeconds;

        // Get RAM usage
        var ramUsage = process.WorkingSet64 / 1024 / 1024; // Convert to MB

        // Check if the system is healthy
        var isHealthy = ramUsage < 1024; // Example: Consider system healthy if RAM usage is less than 1GB

        var data = new Dictionary<string, object>
        {
            { "cpu_usage_seconds", cpuUsage },
            { "ram_usage_mb", ramUsage }
        };

        if (isHealthy)
        {
            return Task.FromResult(HealthCheckResult.Healthy("System is healthy", data));
        }
        else
        {
            return Task.FromResult(HealthCheckResult.Unhealthy("System is under heavy load", null, data));
        }
    }
}