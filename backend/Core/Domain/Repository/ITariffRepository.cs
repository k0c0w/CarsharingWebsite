using Domain.Entities;
using Domain.Repository;

namespace Entities.Repository
{
    public interface ITariffRepository : IRepository<Tariff, int>
    {
        Task<IEnumerable<Tariff>> GetAllActiveAsync();
    }

    public class TariffRepository : ITariffRepository
    {
        public Task<Tariff?> GetByIdAsync(int primaryKey)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tariff>> GetBatchAsync(int? offset = default, int? limit = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddAsync(Tariff entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Tariff entity)
        {
            throw new NotImplementedException();
        }

        public Task<Tariff> RemoveByIdAsync(int primaryKey)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tariff>> GetAllActiveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
