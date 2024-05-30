using System.Text.Json.Serialization;

namespace GraphQL.API.ViewModels;

public class ErrorsVM
{
    [JsonPropertyName("code")]
    [JsonPropertyOrder(1)]
    public int Code { get; set; }
    
    [JsonPropertyName("messages")]
    [JsonPropertyOrder(2)]
    public IEnumerable<string>? Messages { get; set; }
}