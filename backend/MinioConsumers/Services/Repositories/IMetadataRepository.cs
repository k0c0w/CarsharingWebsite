using MinioConsumer.Models;
using FileInfo = MinioConsumer.Models.FileInfo;

namespace MinioConsumer.Services.Repositories;

public interface IMetadataRepository<TMetada> where TMetada : MetadataBase
{
    public Task<bool> MetadataExistsByIdAsync(Guid id);

    public Task<TMetada?> GetByIdAsync(Guid id);

    public Task<Guid> AddAsync(TMetada metadata);

    public Task RemoveByIdAsync(Guid id);
}
