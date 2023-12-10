using Entities.Entities;
using Entities.Repository;
using Microsoft.EntityFrameworkCore;
using Migrations.CarsharingApp;

namespace Persistence.RepositoryImplementation;

public class OccasionRepository : IOccasionRepository
{
    private readonly CarsharingContext _ctx;

    public OccasionRepository(CarsharingContext context)
    {
        _ctx = context;
    }

    public async Task<Guid> AddAsync(Occassion entity)
    {
        await _ctx.Occasions.AddAsync(entity);
        await _ctx.SaveChangesAsync();

        return entity.Id;
    }

    public Task<IEnumerable<Occassion>> GetBatchAsync(int? offset = null, int? limit = null)
    {
        throw new NotImplementedException();
    }

    public Task<Occassion?> GetByIdAsync(Guid primaryKey)
    {
        return _ctx.Occasions.FirstOrDefaultAsync(x => x.Id == primaryKey);
    }

    public async Task<IEnumerable<Occassion>> GetByIssuerIdAsync(Guid issuerId)
    {
        return await _ctx.Occasions
            .Where(x => x.IssuerId == issuerId.ToString())
            .ToArrayAsync();
    }

    public Task<Occassion?> GetOpenOccasionByIssuerIdAsync(Guid issuerId)
    {
        var strId = issuerId.ToString();
        return _ctx.Occasions
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.IssuerId == strId && x.CloseDateUtc == null);
    }

    public Task RemoveByIdAsync(Guid primaryKey)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Occassion entity)
    {
        _ctx.Occasions.Update(entity);
        return _ctx.SaveChangesAsync();
    }

    async Task<IEnumerable<Occassion>> IOccasionRepository.GetOpenedOccasionsAsync()
    {
        return await _ctx.Occasions
            .Where(x => x.CloseDateUtc == null)
            .AsNoTracking()
            .ToArrayAsync();
    }
}
