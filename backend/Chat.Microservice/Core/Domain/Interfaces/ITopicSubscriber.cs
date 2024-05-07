namespace Domain.Interfaces;

public interface ITopicSubscriber
{
    Task ReceiveAsync(MessageAggregate message, CancellationToken ct = default);
}
