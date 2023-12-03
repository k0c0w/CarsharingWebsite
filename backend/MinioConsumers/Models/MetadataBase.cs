namespace MinioConsumer.Models;

public abstract record MetadataBase
{
    public Guid Id { get; init; }

    public string ExternalTag { get; init; }

    public string Schema { get; init; }

    public FileInfo? LinkedFileInfo { get; init; }
}
