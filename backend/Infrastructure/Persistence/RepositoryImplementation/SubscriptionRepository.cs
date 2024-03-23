using Domain.Entities;
using Entities.Exceptions;
using Entities.Repository;
using Microsoft.EntityFrameworkCore;
using Migrations.CarsharingApp;


namespace Persistence.RepositoryImplementation;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly CarsharingContext _ctx;

    public SubscriptionRepository(CarsharingContext context)
    {
        _ctx = context;
    }

    public async Task AddAsync(Subscription entity)
    {
        await _ctx.AddAsync(entity);
    }

    public async Task<IEnumerable<Subscription>> GetActiveSubscriptionsByCarIdAsync(int carId)
    {
        return await
            _ctx.Subscriptions
            .AsNoTracking()
            .Where(x => x.CarId == carId)
            .Where(x => x.IsActive)
            .ToArrayAsync();
    }

    public async Task<IEnumerable<Subscription>> GetBatchAsync(int? offset = null, int? limit = null)
    {
        IQueryable<Subscription> subs = _ctx.Subscriptions.AsNoTracking();

        if (offset.HasValue)
            subs = subs.Skip(offset.Value);

        if (limit.HasValue)
            subs = subs.Take(limit.Value);

        return await subs.ToArrayAsync();
    }

    public Task<Subscription?> GetByIdAsync(int primaryKey)
    {
        return _ctx.Subscriptions
            .SingleOrDefaultAsync(x => x.SubscriptionId == primaryKey);
    }

    public async Task<IEnumerable<Subscription>> GetSubscriptionsByCarIdAsync(int carId)
    {
        return await _ctx.Subscriptions
           .AsNoTracking()
           .Where(x => x.CarId == carId)
           .ToArrayAsync();
    }

    public Task RemoveAsync(Subscription subscription)
    {
        _ctx.Subscriptions.Remove(subscription);

        return Task.CompletedTask;
    }

    public async Task RemoveByIdAsync(int primaryKey)
    {
        var sub = await GetByIdAsync(primaryKey) ?? throw new NotFoundException("Subscription was not found.");

        _ctx.Subscriptions.Remove(sub);
    }

    public Task UpdateAsync(Subscription entity)
    {
        _ctx.Subscriptions.Update(entity);

        return Task.CompletedTask;
    }
}