using System.Text.Json.Serialization;

namespace GraphQL.API.ViewModels.Account;

public record ResponseBearerTokenVm
(
    [property: JsonPropertyName("bearer_token")] string BearerToken
    // public ResponseBearerTokenVm(string bearerToken) => BearerToken = bearerToken;
);