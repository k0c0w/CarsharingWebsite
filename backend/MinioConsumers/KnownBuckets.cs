﻿namespace MinioConsumer;

public static class KnownBuckets
{
    private static Random Random = new Random();

    public const string DOCUMENTS = "documents";

    public static IReadOnlyList<string> BucketPool = new[] { "zebra", "mongoose", "elephant", "monkey", "pantera" };

    public static string GetRandomBucketFromPool()
    {
        return BucketPool[Random.Next(BucketPool.Count)];
    }
}