using Domain;
using LinqToDB;

namespace Persistence.DataAccess;

public class StatisticsRepository(ClickHouseDb db)
    : IStatisticsRepository
{
    public async Task AddSubscriptionInfoAsync(SubscriptionInfo subscriptionInfo)
    {
        _ = await db.TariffsUsage
            .InsertAsync(() => new SubscriptionInfo
            {
                CarLicensePlate = subscriptionInfo.CarLicensePlate,
                CarModelName = subscriptionInfo.CarModelName,
                TariffName = subscriptionInfo.TariffName,
                SubscriptionStartDate = subscriptionInfo.SubscriptionStartDate,
                SubscriptionCreationDate = subscriptionInfo.SubscriptionCreationDate,
            });
    }

    public async Task<IEnumerable<TariffUsageSlice>> GetTariffsPerDayUsageAsync(DateOnly date)
    {
        return await db.TariffsUsage
            .Where(x => x.SubscriptionCreationDate == date)
            .GroupBy(x => x.TariffName)
            .Select(x => new TariffUsageSlice
            {
                TariffName = x.Key,
                UsageCount = x.Count()
            })
            .ToArrayAsync();
    }
}
