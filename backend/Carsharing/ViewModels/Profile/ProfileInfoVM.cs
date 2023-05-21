using System.Text.Json.Serialization;

namespace Carsharing.ViewModels.Profile;

public record ProfileInfoVM
{
    [JsonPropertyName("user_info")]
    [JsonPropertyOrder(1)]
    public UserInfoVM UserInfo { get; init; }

    [JsonPropertyName("booked_cars")]
    [JsonPropertyOrder(2)]
    public IEnumerable<ProfileCarVM> BookedCars { get; init; }
}