using MinioConsumer.Models;

namespace MinioConsumer.Services.Repositories;

public class MongoDbMetadataRepository<TMeatadata> : IMetadataRepository<TMeatadata> where TMeatadata : MetadataBase
{
    public Task<Guid> AddAsync(TMeatadata metadata)
    {
        throw new NotImplementedException();
    }

    public Task<TMeatadata?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> MetadataExistsByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task RemoveByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
