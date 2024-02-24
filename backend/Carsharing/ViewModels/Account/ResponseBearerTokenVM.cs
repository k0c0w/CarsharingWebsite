using System.Text.Json.Serialization;

namespace Carsharing.Forms;

public record ResponseBearerTokenVm
(
    [property: JsonPropertyName("bearer_token")] string BearerToken
    // public ResponseBearerTokenVm(string bearerToken) => BearerToken = bearerToken;
);