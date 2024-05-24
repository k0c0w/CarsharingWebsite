using Carsharing.Contracts.CarEvents;
using Domain;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Persistence.Transport.Consumers;

public class CarBookedEventConsumer(IStatisticsRepository statisticsRepository) : IConsumer<CarBookedEvent>
{
    public async Task Consume(ConsumeContext<CarBookedEvent> context)
    {
        var message = context.Message;

        var subscriptionInfo = new SubscriptionInfo
        {
            CarLicensePlate = message.CarLicensePlate,
            CarModelName = message.CarModelName,
            SubscriptionCreationDate = DateOnly.FromDateTime(message.CreationTimeUtc),
            SubscriptionStartDate = DateOnly.FromDateTime(message.SubscriptionStartTimeUtc),
            TariffName = message.TariffName,
        };

        await statisticsRepository.AddSubscriptionInfoAsync(subscriptionInfo);
    }
}
