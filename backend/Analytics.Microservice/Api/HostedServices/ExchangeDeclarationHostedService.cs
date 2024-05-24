
using Persistence.Transport;
using RabbitMQ.Client;

namespace Analytics.Microservice.HostedServices;

public class ExchangeDeclarationHostedService(IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();

        await using var scope = serviceScopeFactory.CreateAsyncScope();
        var transportModel = scope.ServiceProvider.GetService<IModel>();
        var logger = scope.ServiceProvider.GetService<ILogger<ExchangeDeclarationHostedService>>();

        try
        {
            transportModel.ExchangeDeclare(exchange: QueueConstants.STATISTICS_FANOUT_EXCHANGE, type: ExchangeType.Fanout, durable: false, autoDelete: false);
            transportModel.ExchangeDeclare(exchange: QueueConstants.STATISTICS_DIRECT_EXCHANGE, type: ExchangeType.Direct, durable: false, autoDelete: false);
        }
        catch(Exception ex)
        {
            logger?.LogError("Error while declaring exchanges: {ex}", ex);
            Environment.Exit(1);
        }
    }
}
