namespace Domain.Repository;

public interface IRepository<TEntity, TPrimaryKey>
{
    Task<TEntity?> GetByIdAsync(TPrimaryKey primaryKey);

    Task<IEnumerable<TEntity>> GetBatchAsync(int? offset = default, int? limit = default);

    Task<TPrimaryKey> AddAsync(TEntity entity);

    Task UpdateAsync(TEntity entity);

    Task RemoveByIdAsync(TPrimaryKey primaryKey);
}
