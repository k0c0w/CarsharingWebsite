using Domain;

namespace Analytics.Microservice.HostedServices;

public class StatisticsNotifierBackgroundWorker(IServiceScopeFactory serviceScopeFactory, ILogger<StatisticsNotifierBackgroundWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await WaitForInfrastructureAsync(stoppingToken);

        var sleepTimespan = TimeSpan.FromSeconds(15);
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var todayDate = DateOnly.FromDateTime(DateTime.UtcNow);
                await using var scope = serviceScopeFactory.CreateAsyncScope();

                var repository = scope.ServiceProvider.GetService<IStatisticsRepository>()!;
                var stats = await repository.GetTariffsPerDayUsageAsync(todayDate);

                var notifier = scope.ServiceProvider.GetService<IStatisticsPublisher>()!;
                await notifier.PublishStatistics(stats);

                await scope.DisposeAsync();
                await Task.Delay(sleepTimespan, stoppingToken);
            }
            catch(TaskCanceledException)
            {
                break;
            }
            catch(Exception ex)
            {
                logger.LogError("Error while processing updates: {ex}", ex);
            }
        }
    }

    private async Task WaitForInfrastructureAsync(CancellationToken stoppingToken)
    {
        try
        {
            await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
        }
        catch (TaskCanceledException)
        {
            return;
        }
    }
}
