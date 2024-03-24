using Domain.Repository;
using Migrations.CarsharingApp;

namespace Persistence.UnitOfWork;

public class CarsharingUnitOfWork<TRepository> : IUnitOfWork<TRepository>
{
    private readonly CarsharingContext _ctx;

    public CarsharingUnitOfWork(TRepository repository, CarsharingContext context)
    {
        Unit = repository;
        _ctx = context;
    }

    public TRepository Unit { get; }

    public Task SaveChangesAsync()
        => _ctx.SaveChangesAsync();
}
