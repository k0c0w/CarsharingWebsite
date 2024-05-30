namespace MinioConsumer.Models;
// info about files received
public class FileInfo
{
    public string BucketName { get; set; }

    public string ObjectName { get; set; }

    public string OriginalFileName { get; set; }

    public string ContentType { get; set; }

    public string TargetBucketName { get; set; }
}