using Domain.Entities;
using Entities.Exceptions;
using Entities.Repository;
using Microsoft.EntityFrameworkCore;
using Migrations.CarsharingApp;

namespace Persistence.RepositoryImplementation;

public class TariffRepository : ITariffRepository
{
    private readonly CarsharingContext _ctx;

    public TariffRepository(CarsharingContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<Tariff?> GetByIdAsync(int primaryKey)
    {
        return await _ctx.Tariffs
            .AsNoTracking()
            .SingleAsync(x => x.TariffId == primaryKey);
    }

    public async Task<IEnumerable<Tariff>> GetBatchAsync(int? offset = default, int? limit = default)
    {
        IQueryable<Tariff> messages = _ctx.Tariffs;

        if (offset != null)
            messages = messages.Skip(offset.Value);
        if (limit != null)
            messages = messages.Take(limit.Value);

        return await messages
            .AsNoTracking()
            .ToArrayAsync();
    }

    public async Task AddAsync(Tariff entity)
    {
        await _ctx.Tariffs.AddAsync(entity);
    }

    public Task UpdateAsync(Tariff entity)
    {
        _ctx.Tariffs.Update(entity);

        return Task.CompletedTask;
    }

    public async Task RemoveByIdAsync(int primaryKey)
    {
        var tariff = await GetByIdAsync(primaryKey) ?? throw new NotFoundException($"Tariff was not found: {primaryKey}", typeof(Tariff));

        _ctx.Tariffs.Remove(tariff);
    }

    public async Task<IEnumerable<Tariff>> GetAllActiveAsync()
    {
        return await _ctx.Tariffs
            .AsNoTracking()
            .Where(x => x.IsActive)
            .ToArrayAsync(); ;
    }
}