using System.Text.Json.Serialization;

namespace Features.Occasion;

public class OccasionMessageDto
{
    public Guid Id { get; init; }

    [JsonPropertyName("text")]
    public string MessageText { get; init; }

    [JsonPropertyName("authorName")]
    public string AuthorName { get; init; }
    
    public bool IsFromManager { get; init; }

    public IEnumerable<OccasionMessageAttachmentDto> Attachments { get; set; }
}


public class OccasionMessageAttachmentDto
{
    [JsonPropertyName("download_url")]
    public string DownloadUrl { get; init; }

    [JsonPropertyName("content_type")]
    public string ContentType { get; init; }

    [JsonPropertyName("file_name")]
    public string FileName { get; init; }
}