using Domain;
namespace Persistence.Transport;

public record TariffsUsageStatisticsEvent
{
    public IEnumerable<TariffUsageSlice> Stats { get; init; } = [];
}