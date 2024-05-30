using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Migrations.CarsharingApp;

namespace Main.Service.Worker.HostedServices;

internal class SubscriptionChecker(IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();
        var waitSpan = TimeSpan.FromMinutes(1);
        while(!stoppingToken.IsCancellationRequested)
        {
            await using var scope = serviceScopeFactory.CreateAsyncScope();
            try
            {
                await ReleaseSubscriptionsAsync(scope.ServiceProvider.GetService<CarsharingContext>()!);

            }
            catch(Exception ex)
            {
                var logger = scope.ServiceProvider.GetService<ILogger<SubscriptionChecker>>();
                logger?.LogError("Error while releasing subscriptions: {ex}", ex);
            }
            finally
            {
                await scope.DisposeAsync();
            }

            await DelayOrCancelAsync(waitSpan, stoppingToken);
        }
    }

    private Task ReleaseSubscriptionsAsync(CarsharingContext context)
    {
        return ReleaseSubscriptionsAsync(context, GetExpiredSubscriptions(context), partitions: 1024);
    }

    private async Task ReleaseSubscriptionsAsync(CarsharingContext context, IQueryable<Subscription> expired, int partitions)
    {
        while (await expired.AnyAsync())
        {
            var subs = await expired
                .Take(partitions)
                .Include(x => x.Car)
                .ToListAsync();

            foreach (var sub in subs)
            {
                sub.IsActive = false;
                sub.Car!.IsTaken = false;
            }

            context.Subscriptions.UpdateRange(subs);
            await context.SaveChangesAsync();
        }
    }

    private IQueryable<Subscription> GetExpiredSubscriptions(CarsharingContext context)
    => context.Subscriptions
        .Where(x => x.IsActive)
        .Where(x => DateTime.Today > x.EndDate);

    private async Task DelayOrCancelAsync(TimeSpan delaySpan, CancellationToken cancellationToken)
    {
        try
        {
            await Task.Delay(delaySpan, cancellationToken);
        }
        catch(TaskCanceledException)
        {
        } 
    }
}
