namespace Services.Abstractions.Repositories;

public interface IRepository<TId, TModel>
{
    Task<TModel?> GetByIdAsync(TId id);

    Task<IEnumerable<TModel>> GetAllAsync(Func<TModel, bool>? predicate = null);

    Task AddAsync(TModel model);

    Task RemoveByIdAsync(TId id);

    Task UpdateAsync(TModel model);
}
