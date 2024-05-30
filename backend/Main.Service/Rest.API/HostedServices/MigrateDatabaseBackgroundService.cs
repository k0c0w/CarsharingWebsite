using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Carsharing.HostedServices;

public class MigrateDatabaseBackgroundService<TDbContext>(IServiceScopeFactory scopeFactory, ILogger<MigrateDatabaseBackgroundService<TDbContext>> logger) 
    : BackgroundService where TDbContext : DbContext
{
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;

    private readonly ILogger<MigrateDatabaseBackgroundService<TDbContext>> _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Applying migrations for {DbContext}", TypeCache<TDbContext>.ShortName);

        using var scope = _scopeFactory.CreateScope();
        try
        {
            using var context = scope.ServiceProvider.GetRequiredService<TDbContext>();
            await context.Database.MigrateAsync(cancellationToken: stoppingToken);

            _logger.LogInformation("Migrations completed for {DbContext}", TypeCache<TDbContext>.ShortName);
        }
        catch (Exception ex)
        {
            _logger.LogCritical("Could not migrate database for {DbContext}: {ex}", TypeCache<TDbContext>.ShortName, ex);
            Environment.Exit(1);
        }
    }
}
