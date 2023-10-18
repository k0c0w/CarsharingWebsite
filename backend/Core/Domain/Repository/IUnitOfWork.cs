namespace Domain.Repository;

public interface IUnitOfWork
{

    Task SaveChangesAsync();
}
