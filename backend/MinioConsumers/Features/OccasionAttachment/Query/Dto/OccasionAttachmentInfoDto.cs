using System.Text.Json.Serialization;

namespace MinioConsumer.Features.OccasionAttachment.Query.Dto;

public class OccasionAttachmentInfoDto
{
    [JsonPropertyName("uploaded_at")]
    public DateTime CreationDateUtc { get; init; }

    [JsonPropertyName("uploader")]
    public Guid UploaderId { get; init; }

    [JsonPropertyName("attachments")]
    public IEnumerable<AttachmentInfoDto> Attachments { get; init; } = Array.Empty<AttachmentInfoDto>();
}

public class AttachmentInfoDto
{
    [JsonPropertyName("content_type")]
    public string ContentType { get; set; }

    [JsonPropertyName("download_url")]
    public string DownloadUrl { get; set; }

    [JsonPropertyName("file_name")]
    public string FileName { get; set; }
}