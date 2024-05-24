namespace Services.Abstractions;

public interface ITariffDayUsagePublisherService
{
    public Task PublishStatistics(DateOnly day);
}
