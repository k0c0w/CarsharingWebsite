using Domain;
using Persistence.Transport;
using static Persistence.Transport.QueueConstants;
using System.Text.Json;
using System.Text;
using RabbitMQ.Client;

namespace Persistence.Services;

public class StatisticsPublisher(IModel channel) : IStatisticsPublisher
{
    public Task PublishStatistics(IEnumerable<TariffUsageSlice> tariffUsageSlices, string? destination = null)
    {
        var message = new TariffsUsageStatisticsEvent { Stats = tariffUsageSlices };

        channel.BasicPublish(
            exchange: string.IsNullOrEmpty(destination) ? STATISTICS_FANOUT_EXCHANGE : STATISTICS_DIRECT_EXCHANGE,
            routingKey: destination ?? string.Empty,
            basicProperties: null,
            body: Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message))
        );

        return Task.CompletedTask;
    }
}
