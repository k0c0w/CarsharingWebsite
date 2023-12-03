using MinioConsumer.Models;

namespace MinioConsumer.Services.PrimaryStorageSaver
{
    public record SaveInPRimaryDbRequest<TMetadata>(Guid OperationId, Guid MetadataId) where TMetadata : MetadataBase;
}
