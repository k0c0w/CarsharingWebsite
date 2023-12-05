using System.Text.Json.Serialization;

namespace MinioConsumer.Features.Documents.Query;

public class DocumentMetadataDto
{
    [JsonPropertyName("annotation")]
    public string DisplayableHeader { get; set; }

    [JsonPropertyName("file_name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("date")]
    public DateTime CreationDate { get; set; }

    [JsonPropertyName("download_url")]
    public string Url { get; set; } = string.Empty;
}

public class AdminDocumentMetadataDto : DocumentMetadataDto
{
    public bool IsPrivate { get; set; }
}