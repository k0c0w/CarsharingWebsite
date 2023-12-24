namespace MinioConsumer.Services;

public static class KnownBuckets
{
    private static Random Random = new Random();

    public const string DOCUMENTS = "documents";

    public const string OCCASIONATTACHMENTS = "occasion-attachments";

    public static IReadOnlyList<string> BucketPool = new[] { "zebra", "mongoose", "elephant", "monkey", "pantera" };

    public static string GetRandomBucketFromPool()
    {
        return BucketPool[Random.Next(BucketPool.Count)];
    }
}
