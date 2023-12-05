﻿namespace MinioConsumer.Models;

public record DocumentMetadata : MetadataBase
{
    public DateTime CreationDateTimeUtc { get; init; }

    public string ContentType { get; init; }

    public string BucketName { get; } = KnownBuckets.DOCUMENTS;

    public string FileName { get; init; }

    public bool IsPublic { get; init; }

    public DocumentMetadata(Guid id, FileInfo? linkedFileInfo) : base(id, KnownBuckets.DOCUMENTS, linkedFileInfo)
    {
    }
}