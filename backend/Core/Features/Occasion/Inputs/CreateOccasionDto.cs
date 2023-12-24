using System.Text.Json.Serialization;

namespace Features.Occasion.Inputs;

public class CreateOccasionDto
{
    [JsonPropertyName("topic")]
    public string Topic { get; set; }

    [JsonPropertyName("event")]
    public string OccasionType { get; set; }
}
