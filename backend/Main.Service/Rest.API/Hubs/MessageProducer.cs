using System.Text;
using Domain.Common;
using RabbitMQ.Client;
using System.Text.Json;
using MassTransit;

namespace Carsharing.ChatHub;

public class MessageProducer : IMessageProducer
{
    private readonly IPublishEndpoint _publishEndpoint;

    public MessageProducer(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }
    
    public async Task SendMessageAsync<T>(
        T message, 
        CancellationToken cancellationToken = default
        ) where T: class 
        => await _publishEndpoint.Publish(message, cancellationToken);
}