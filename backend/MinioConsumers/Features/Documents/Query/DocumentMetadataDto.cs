using System.Text.Json.Serialization;

namespace MinioConsumer.Features.Documents.Query;

public class DocumentMetadataDto
{
    public Guid Id { get; init; }

    [JsonPropertyName("annotation")]
    public string DisplayableHeader { get; init; }

    [JsonPropertyName("file_name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("date")]
    public DateTime CreationDate { get; init; }

    [JsonPropertyName("download_url")]
    public string Url { get; init; } = string.Empty;
}

public class AdminDocumentMetadataDto : DocumentMetadataDto
{
    [JsonPropertyName("isPrivate")]
    public bool IsPrivate { get; init; }
}