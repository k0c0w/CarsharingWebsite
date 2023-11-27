namespace Domain.Common;

public interface IFileProducer
{
    Task SendFileAsync<T>(T message, CancellationToken cancellationToken = default) where T: class; 
}