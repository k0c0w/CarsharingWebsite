using System.Text.Json.Serialization;

namespace Carsharing.ViewModels.Admin.User
{
    public class OpenChatsVM
    {
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [JsonPropertyName("last_name")]
        public string LastName { get; set; } = string.Empty;

        [JsonPropertyName("user_id")]
        public string UserId { get; set; } = string.Empty;

        [JsonPropertyName("connection_id")]
        public string ConnectionId { get; set; } = string.Empty;
    }
}
