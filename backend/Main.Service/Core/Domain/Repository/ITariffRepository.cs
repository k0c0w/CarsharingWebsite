using Domain.Entities;
using Domain.Repository;

namespace Entities.Repository
{
    public interface ITariffRepository : IRepository<Tariff, int>
    {
        Task<IEnumerable<Tariff>> GetAllActiveAsync();
    }
}
