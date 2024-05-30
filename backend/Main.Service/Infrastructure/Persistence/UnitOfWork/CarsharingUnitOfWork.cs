using Domain.Repository;
using Migrations.CarsharingApp;

namespace Persistence.UnitOfWork;

public class CarsharingUnitOfWork : IUnitOfWork
{
    private readonly CarsharingContext _ctx;

    public CarsharingUnitOfWork(CarsharingContext context)
    {
        _ctx = context;
    }

    public Task SaveChangesAsync()
        => _ctx.SaveChangesAsync();
}
