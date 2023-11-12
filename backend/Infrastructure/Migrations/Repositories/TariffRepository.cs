using Domain.Entities;
using Entities.Repository;
using Microsoft.EntityFrameworkCore;
using Migrations.CarsharingApp;

namespace Migrations.Repositories;

public class TariffRepository : ITariffRepository
{
    private readonly CarsharingContext _ctx;

    public TariffRepository(CarsharingContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<Tariff?> GetByIdAsync(int primaryKey)
    {
        var single = await _ctx.Tariffs.SingleAsync(x => x.TariffId == primaryKey);
        await _ctx.SaveChangesAsync();
        return single;
    }

    public async Task<IEnumerable<Tariff>> GetBatchAsync(int? offset = default, int? limit = default)
    {
        IQueryable<Tariff> messages = _ctx.Tariffs;

        if (offset != null)
            messages = messages.Skip(offset.Value);
        if (limit != null)
            messages = messages.Skip(limit.Value);

        return await messages.ToArrayAsync().ConfigureAwait(false);
    }

    public async Task<int> AddAsync(Tariff entity)
    {
        var elem = await _ctx.Tariffs.AddAsync(entity);
        await _ctx.SaveChangesAsync();
        return elem.Entity.TariffId;
    }

    public async Task UpdateAsync(Tariff entity)
    {
        await _ctx.Tariffs
            .Where(e=>e.TariffId == entity.TariffId)
            .ExecuteUpdateAsync(setPropCalls => setPropCalls
                .SetProperty(tariff => tariff.Description, e => entity.Description)
                .SetProperty(tariff => tariff.Name, e => entity.Name)
                .SetProperty(tariff => tariff.ImageUrl, e => entity.ImageUrl)
                .SetProperty(tariff => tariff.Price, e => entity.Price)
                .SetProperty(tariff => tariff.IsActive, e => entity.IsActive)
                .SetProperty(tariff => tariff.MaxMileage, e => entity.MaxMileage));
        
        await _ctx.SaveChangesAsync();
    }

    public async Task<Tariff> RemoveByIdAsync(int primaryKey)
    {
        var tariff = await _ctx.Tariffs
            .SingleOrDefaultAsync(e => e.TariffId == primaryKey);
        
        if (tariff is null)
            return new Tariff();
        
        _ctx.Tariffs.Remove(tariff);
        await _ctx.SaveChangesAsync();
        return tariff;
    }

    public async Task<IQueryable<Tariff>> GetAllActiveAsync()
    {
        var tariffs = _ctx.Tariffs.Where(x => x.IsActive);
        return tariffs;
    }
}