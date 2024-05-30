namespace Domain;

public class SubscriptionInfo
{
    public string TariffName { get; init; } = string.Empty;

    public string CarModelName { get; init; } = string.Empty;

    public string CarLicensePlate { get; init; } = string.Empty;

    public DateOnly SubscriptionStartDate { get; init; }

    public DateOnly SubscriptionCreationDate { get; init; }
}
