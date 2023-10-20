namespace Domain.Common;

public interface IMessageProducer
{
    Task SendMessage<T>(T message, CancellationToken cancellationToken = default) where T: class; 
}