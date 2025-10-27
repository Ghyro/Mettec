using MettecService.API.Options;
using MettecService.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MettecService.API.StartupExtensions;

public static class DatabaseSetupExtensions
{
    public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PostgresOptions>(configuration.GetSection("Postgres"));
        
        services.AddDbContext<MettecDbContext>((serviceProvider, options) =>
        {
            var pgOptions = serviceProvider.GetRequiredService<IOptions<PostgresOptions>>().Value;
            options.UseNpgsql(pgOptions.GetConnectionString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
    }

    public static void ApplyDatabaseMigrationsIfNeeded(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<MettecDbContext>();
        var pendingMigrations = db.Database.GetPendingMigrations();
        if (pendingMigrations.Any())
            db.Database.Migrate();
    }
}