using MinioConsumer.Services;

namespace MinioConsumer.BackgroundServices;

public class TempFileCleanerBackgroundService : BackgroundService
{

    private readonly IServiceProvider _serviceProvider;

    public TempFileCleanerBackgroundService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                var scopedProcessingService = scope.ServiceProvider.GetRequiredService<IMetadataScopedProcessingService>();

                await scopedProcessingService.CleanTempFoldersAsync(stoppingToken);
            }

            await Task.Delay(TimeSpan.FromHours(4), stoppingToken);
        }
    }
}

