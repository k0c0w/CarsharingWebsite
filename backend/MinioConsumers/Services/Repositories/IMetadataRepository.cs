using MinioConsumer.Models;
using FileInfo = MinioConsumer.Models.FileInfo;

namespace MinioConsumer.Services.Repositories;

public interface IMetadataRepository
{

    public Task<bool> MetadataExists(Guid guid);

    public Task<MetadataBase?> GetById(Guid id);

    public Task<MetadataBase> Add(MetadataBase metadata);

    public Task UpdateFileInfo(Guid metadataGuid, FileInfo file);

    public Task<bool> IsCompletedById(Guid metadataGuid);

    public Task<MetadataBase> RemoveById(Guid guid);
}
