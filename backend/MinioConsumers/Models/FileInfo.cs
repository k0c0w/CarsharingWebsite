namespace MinioConsumer.Models;
// info about files received
public class FileInfo
{
    public bool IsTemporary { get; init; }

    public string BucketName { get; init; }

    public string ObjectName { get; init; }

    public string OriginalFileName { get; init; }

    public string ContentType { get; init; }
}