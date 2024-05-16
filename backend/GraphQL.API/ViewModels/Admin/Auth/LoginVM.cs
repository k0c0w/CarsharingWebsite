using System.Text.Json.Serialization;

namespace GraphQL.API.ViewModels.Admin.Auth
{
    public record LoginAdminVM
    (
        IEnumerable<string> Roles,
        [property: JsonPropertyName("bearer_token")] string BearerToken
    );
}
