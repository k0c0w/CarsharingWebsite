namespace MinioConsumer.Models;

public record MetadataBase
{
    public Guid Id { get; init; }

    public string ExternalTag { get; init; }

    public string Schema { get; init; }

    // metadata for file group
    public FileInfo? LinkedFileInfo { get; set; }

    public MetadataBase(Guid id, string externalTag, string schema, FileInfo? linkedFileInfo)
    {
        Id = id;
        ExternalTag = externalTag;
        Schema = schema;
        LinkedFileInfo = linkedFileInfo;
    }

    public MetadataBase()
    {
    }
}
