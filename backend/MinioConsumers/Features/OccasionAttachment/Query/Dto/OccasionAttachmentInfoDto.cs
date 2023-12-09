using System.Text.Json.Serialization;

namespace MinioConsumer.Features.OccasionAttachment.Query.Dto;

public class OccasionAttachmentInfoDto
{
    [JsonPropertyName("uploaded_at")]
    public DateTime CreationDateUtc { get; init; }

    [JsonPropertyName("uploader")]
    public Guid UploaderId { get; init; }

    [JsonPropertyName("attachments")]
    public IEnumerable<string> AttachmentsUrls { get; init; } = Array.Empty<string>();
}