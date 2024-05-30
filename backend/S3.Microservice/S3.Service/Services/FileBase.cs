namespace MinioConsumer.Services;

public record class S3File
{
    public Stream ContentStream { get; }

    public long Length => ContentStream.Length;

    public string Name { get; }

    public string BucketName { get; }

    public string ContentType { get; }

    public S3File(string fileName, string bucketName, Stream content, string contentType)
    {
        ContentStream = content;
        Name = fileName;
        ContentType = contentType;
        BucketName = bucketName;
    }

    protected virtual void DisposeInternal()
    {
        ContentStream?.Dispose();
    }

    public void Dispose()
    {
        DisposeInternal();
    }
}
