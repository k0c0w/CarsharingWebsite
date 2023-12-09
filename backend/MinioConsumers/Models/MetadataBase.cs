namespace MinioConsumer.Models;

public record MetadataBase
{
    public Guid Id { get; init; }

    public string Schema { get; init; }

    // metadata for file group
    public List<FileInfo> LinkedFileInfos { get; set; } = new List<FileInfo>();

    public int LinkedMetadataCount { get; set; }

    public MetadataBase(Guid id, string schema, int targetFilesCount,  List<FileInfo>? linkedFileInfos = default)
    {
        Id = id;
        Schema = schema;
        LinkedMetadataCount = targetFilesCount;
        LinkedFileInfos ??= linkedFileInfos;
    }
}
