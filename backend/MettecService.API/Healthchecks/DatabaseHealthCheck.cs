using MettecService.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MettecService.API.Healthchecks;

/// <inheritdoc />
public class DatabaseHealthCheck(MettecDbContext context, ILogger<DatabaseHealthCheck> logger)
    : IHealthCheck
{
    /// <inheritdoc />
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context1, CancellationToken cancellationToken = default)
    {
        try
        {
            await context.Database.OpenConnectionAsync(cancellationToken: cancellationToken);
            await context.Database.CloseConnectionAsync();
            return HealthCheckResult.Healthy("Database connection succeeded");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Database Healthcheck failed with error {ErrorMessage}", ex.Message);
            return HealthCheckResult.Unhealthy("Unable to connect to database");
        }
    }
}