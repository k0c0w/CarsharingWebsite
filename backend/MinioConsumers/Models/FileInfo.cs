namespace MinioConsumer.Models;
// info about files received
public class FileInfo
{
    public bool IsTemporary { get; init; }

    public string BucketName { get; init; }

    public string Name { get; init; }

    public string ContentType { get; init; }
}