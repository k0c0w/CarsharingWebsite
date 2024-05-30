namespace Domain;

public interface IStatisticsPublisher
{
    public Task PublishStatistics(IEnumerable<TariffUsageSlice> tariffUsageSlices, string? destination = null);
}
