using Domain.Common;

namespace CraphQl.ChatHub;

public class FakeMessageProducer : IMessageProducer
{
    public Task SendMessageAsync<T>(
        T message,
        CancellationToken cancellationToken = default
        ) where T : class
        => Task.CompletedTask; 
}