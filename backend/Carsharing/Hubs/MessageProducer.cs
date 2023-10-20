using System.Text;
using Domain.Common;
using RabbitMQ.Client;
using System.Text.Json;
using MassTransit;

namespace Carsharing.ChatHub;

public class MessageProducer : IMessageProducer
{
    private IPublishEndpoint _publishEndpoint;

    public MessageProducer(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }
    
    public async Task SendMessage<T>(T message, CancellationToken cancellationToken = default)
    where T: class
    {
        // var factory = new ConnectionFactory() { HostName = "localhost" };
        // using (var connection = factory.CreateConnection())
        // using (var channel = connection.CreateChannel())
        // {
        //     channel.QueueDeclare(queue: "MyQueue",
        //         durable: false,
        //         exclusive: false,
        //         autoDelete: false,
        //         arguments: null);
        //     var jsonString = JsonSerializer.Serialize(message);
        //     var body = Encoding.UTF8.GetBytes(jsonString);
        //
        //     channel.BasicPublish(exchange:"",
        //         routingKey:"MyQueue",
        //         body: body);
        // }
        await _publishEndpoint.Publish(message, cancellationToken);
    }
}