namespace MinioConsumer.Models;

public record DocumentMetadata : MetadataBase
{
    public DateTime CreationDateTimeUtc { get; init; }

    public string BucketName { get; } = KnownBuckets.DOCUMENTS;

    public bool IsPublic { get; init; }

    public DocumentMetadata(Guid id, FileInfo? linkedFileInfo) : base(id, KnownBuckets.DOCUMENTS, linkedFileInfo)
    {
    }
}
