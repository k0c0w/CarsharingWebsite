namespace MinioConsumer.Models;

public record DocumentMetadata : MetadataBase
{
    public DateTime CreationDateTimeUtc { get; init; }

    public string ContentType { get; init; }

    public string BucketName { get; } = KnownBuckets.DOCUMENTS;

    public string FileName { get; init; }

    public bool IsPublic { get; init; }

    public DocumentMetadata() : base()
    {
        Id = Guid.NewGuid();
    }
    
    public DocumentMetadata(Guid id, string externalTag, string schema, FileInfo? linkedFileInfo) : base(id, externalTag, schema, linkedFileInfo)
    {
    }
}
