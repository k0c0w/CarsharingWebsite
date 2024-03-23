using Domain.Entities;
using Domain.Repository;
using Entities.Exceptions;
using Entities.Repository;
using Microsoft.EntityFrameworkCore;
using Migrations.CarsharingApp;
using Persistence.UnitOfWork;

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
        var single = await _ctx.Tariffs.SingleAsync(x => x.TariffId == primaryKey);
        return single;
    }

    public async Task<IEnumerable<Tariff>> GetBatchAsync(int? offset = default, int? limit = default)
    {
        IQueryable<Tariff> messages = _ctx.Tariffs;

        if (offset != null)
            messages = messages.Skip(offset.Value);
        if (limit != null)
            messages = messages.Take(limit.Value);

        return await messages.ToArrayAsync();
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
                .SetProperty(tariff => tariff.PricePerMinute, e => entity.PricePerMinute)
                .SetProperty(tariff => tariff.IsActive, e => entity.IsActive)
                .SetProperty(tariff => tariff.MaxMileage, e => entity.MaxMileage));
    }

    public async Task RemoveByIdAsync(int primaryKey)
    {
        var effectedRows = await _ctx.Tariffs.Where(x => x.TariffId == primaryKey).ExecuteDeleteAsync();

        if (effectedRows == 0)
            throw new NotFoundException($"Tariff was not found: {primaryKey}", typeof(Tariff));
    }

    public async Task<IEnumerable<Tariff>> GetAllActiveAsync()
    {
        var tariffs = await _ctx.Tariffs.Where(x => x.IsActive).ToArrayAsync();

        return tariffs;
    }
}

public class TariffUnitOfWork : CarsharingUnitOfWorkBase, IUnitOfWork<ITariffRepository>
{
    public ITariffRepository Unit { get; }


    public TariffUnitOfWork(ITariffRepository tariffRepository, CarsharingContext context) : base(context)
    {
        Unit = tariffRepository;
    }
}