using Domain.Repository;
using Entities.Entities;

namespace Entities.Repository;

public interface IOccasionRepository : IRepository<Occassion, Guid>
{
    public Task<IEnumerable<Occassion>> GetByIssuerIdAsync(Guid issuerId);

    public Task<Occassion?> GetOpenOccasionByIssuerIdAsync(Guid issuerId);

    public Task<Occassion?> GetOpenedOccasionsAsync();
}
