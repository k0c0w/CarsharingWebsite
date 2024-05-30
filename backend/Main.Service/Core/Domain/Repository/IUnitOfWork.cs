namespace Domain.Repository;

public interface IUnitOfWork
{

    Task SaveChangesAsync();
}

public interface IUnitOfWork<TRepository>
{
    TRepository Unit { get; }

    Task SaveChangesAsync();
}
