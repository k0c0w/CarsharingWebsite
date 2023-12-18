using System.Text.Json.Serialization;

namespace MinioConsumer.Features.Documents.InputModels;

public class EditDocumentInfoDto
{
    [JsonPropertyName("isPublic")]
    public bool? IsPublic { get; set; }

    [JsonPropertyName("annotation")]
    public string? Annotation { get; set; }
}
