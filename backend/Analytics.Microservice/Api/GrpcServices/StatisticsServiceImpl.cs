using Domain;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Persistence.Transport;
using RabbitMQ.Client;
using System.Security.Claims;

namespace Analytics.Microservice.GrpcServices;

[Authorize]
internal class StatisticsServiceImpl(IModel transportModel, IStatisticsPublisher statisticsPublisher, IStatisticsRepository statisticsRepository) : AnalyticsService.AnalyticsServiceBase
{
    public override async Task<SubscriptionInfo> SubscribeTariffsUsageUpdates(Empty request, ServerCallContext context)
    {
        var userId = GetUserId(context);
        var declaredQueue = DeclareQueueForUser(userId);
        await SendInitialStatisticsToUserAsync(declaredQueue);

        return new SubscriptionInfo
        {
            QueueName = declaredQueue
        };
    }

    private string DeclareQueueForUser(string userId)
    {
        var queueName = userId;

        transportModel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: true);
        transportModel.QueueBind(queueName, exchange: QueueConstants.STATISTICS_FANOUT_EXCHANGE, routingKey: string.Empty);
        transportModel.QueueBind(queueName, exchange: QueueConstants.STATISTICS_DIRECT_EXCHANGE, routingKey: userId);

        return queueName;
    }

    private async Task SendInitialStatisticsToUserAsync(string destination)
    {
        var stats = await statisticsRepository.GetTariffsPerDayUsageAsync(DateOnly.FromDateTime(DateTime.UtcNow));
        await statisticsPublisher.PublishStatistics(stats, destination: destination);
    }

    private static string GetUserId(ServerCallContext context) 
        => context.GetHttpContext().User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new InvalidOperationException();
}
