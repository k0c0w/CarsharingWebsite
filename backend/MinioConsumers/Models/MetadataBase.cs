namespace MinioConsumer.Models;

public abstract record MetadataBase
{
    public Guid Id { get; init; }

    public string ExternalTag { get; init; }

    public string Schema { get; init; }

    // metadata for file group
    FileInfo? LinkedFileInfo { get; init; }
}
