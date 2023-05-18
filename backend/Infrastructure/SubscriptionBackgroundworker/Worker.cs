using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SubscriptionBackgroundworker;

internal sealed class Worker
{
    private readonly ILogger logger;
    public Worker(ILogger logger)
    {
        this.logger = logger;
    }

    public async Task Run()
    {
        while (true)
        {
            await CollectExpiredSubscriptionsAsync();

            Console.WriteLine("collected");
            var timeToSleep = GetThreadSleepSpan();
            GC.Collect();
            Thread.Sleep(timeToSleep);
        }
    }

    private TimeSpan GetThreadSleepSpan()
    {
        var today = DateTime.Today;
        var nextDay = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0) + TimeSpan.FromDays(1);
        return nextDay - DateTime.Now;
    }
    
    private async Task CollectExpiredSubscriptionsAsync()
    {
        await using var context = new SubscriptionDbContext();
        var subscriptionsToExpire = GetExpiredSubscriptions(context);
        const int partition = 1024;

        await ReleaseSubscriptionsAsync(context, subscriptionsToExpire, partition);
    }

    private async Task ReleaseSubscriptionsAsync(SubscriptionDbContext context, IQueryable<Subscription> expired,
        int partitions)
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
                sub.Car.IsTaken = false;
            }
            
            context.Subscriptions.UpdateRange(subs);
            await context.SaveChangesAsync();
        }
    }

    private IQueryable<Subscription> GetExpiredSubscriptions(SubscriptionDbContext context)
        => context.Subscriptions
            .Where(x => x.IsActive)   
            .Where(x => DateTime.Today > x.EndDate);
}