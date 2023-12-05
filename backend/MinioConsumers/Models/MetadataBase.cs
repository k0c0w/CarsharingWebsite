namespace MinioConsumer.Models;

public record MetadataBase
{
    public Guid Id { get; init; }


    public string Schema { get; init; }

    // metadata for file group
    public FileInfo? LinkedFileInfo { get; set; }

    public MetadataBase(Guid id, string schema, FileInfo? linkedFileInfo)
    {
        Id = id;
        Schema = schema;
        LinkedFileInfo = linkedFileInfo;
    }
}
