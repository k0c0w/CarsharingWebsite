using MinioConsumer.Services;

namespace MinioConsumer.Models;

public record DocumentMetadata : MetadataBase
{
    public string Annotation { get; set; }

    public string BucketName { get; } = KnownBuckets.DOCUMENTS;

    public bool IsPublic { get; set; }

    public DocumentMetadata(Guid id, FileInfo? linkedFileInfo = default) : base(id, KnownBuckets.DOCUMENTS, 1)
    {
    }
}
