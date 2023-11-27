using Domain.Common;
using MassTransit;

namespace Carsharing.ChatHub;

public class FileProducer : IFileProducer
{
    private readonly IPublishEndpoint _publishEndpoint;

    public FileProducer(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }
    
    public async Task SendFileAsync<T>(T message, CancellationToken cancellationToken = default) where T : class
    {
        await _publishEndpoint.Publish(message, cancellationToken);
    }
}