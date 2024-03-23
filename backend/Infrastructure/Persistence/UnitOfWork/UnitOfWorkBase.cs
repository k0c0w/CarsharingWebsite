using Migrations.CarsharingApp;

namespace Persistence.UnitOfWork;

public abstract class CarsharingUnitOfWorkBase
{
    private readonly CarsharingContext _ctx;

    protected CarsharingUnitOfWorkBase(CarsharingContext context)
    {
        _ctx = context;
    }

    public Task SaveChangesAsync()
        => _ctx.SaveChangesAsync();
}
