using LinqToDB.Data;
using Persistence.DataAccess;

namespace Analytics.Microservice.HostedServices;

public class DatabaseMigrationHostedService(IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();

        await using var scope = serviceScopeFactory.CreateAsyncScope();
        var logger = scope.ServiceProvider.GetService<ILogger<DatabaseMigrationHostedService>>();
        try
        {
            var db = scope.ServiceProvider.GetService<ClickHouseDb>() ?? throw new InvalidOperationException($"Provide {nameof(ClickHouseDb)}");

            await db.ExecuteAsync(DatabaseScripts.CREATE_DATABASE_SQL);
            await db.ExecuteAsync(DatabaseScripts.CREATE_TARIFFS_USAGE_SQL);
        }
        catch (Exception ex)
        {
            logger?.LogError("Error while migrating database with {ex}.", ex);

            Environment.Exit(1);
        }
    }
}
