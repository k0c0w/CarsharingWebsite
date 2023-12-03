using MinioConsumer.Models;
using FileInfo = MinioConsumer.Models.FileInfo;

namespace MinioConsumer.Services.Repositories
{
    public interface ITempMetadataRepository<TMetadata> : IMetadataRepository<TMetadata> where TMetadata : MetadataBase
    {
        public Task UpdateFileInfoAsync(Guid id, FileInfo file);

        public Task<bool> IsCompletedByIdAsync(Guid id);
    }
}
