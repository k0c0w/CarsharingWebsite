
namespace Domain;

public interface IStatisticsRepository
{
    Task AddSubscriptionInfoAsync(SubscriptionInfo subscriptionInfo);

    Task<IEnumerable<TariffUsageSlice>> GetTariffsPerDayUsageAsync(DateOnly date);
}
