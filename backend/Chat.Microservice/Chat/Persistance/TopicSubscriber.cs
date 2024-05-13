using ChatService;
using Domain;
using Domain.Interfaces;
using Grpc.Core;

namespace Chat.Persistance;

// can not put this into separate project due to circular referrences
internal class TopicSubscriber(string assignedTopic, IServerStreamWriter<FromServerMessage> responseStream) : ITopicSubscriber
{
    private readonly string _assignedTopic = assignedTopic;

    private readonly IServerStreamWriter<FromServerMessage> _responseStream = responseStream;

    public async Task ReceiveAsync(MessageAggregate messageAggregate, CancellationToken ct = default)
    {
        var author = messageAggregate.Author;
        var message = messageAggregate.Message;

        var sendMessage = new FromServerMessage()
        {
            Message = new ChatService.Message
            {
                Author = new MessageAuthor
                {
                    Id = author.Id,
                    Name = author.Name,
                    IsManager = author.IsManager,
                },
                Text = message.Text,
            }
        };

        await _responseStream.WriteAsync(sendMessage);
        Console.WriteLine("Send message");
    }

    public async Task NotifyAssignedTopicAsync(CancellationToken ct = default)
    {
        try
        {
            var sendMessage = new FromServerMessage()
            {
                TopicInfo = new TopicInfoMessage
                {
                    TopicName = _assignedTopic
                },
            };
            await _responseStream.WriteAsync(sendMessage, ct);
        }
        catch (TaskCanceledException)
        {

        }
    }

    public override bool Equals(object? obj)
    {
        if (obj is TopicSubscriber topicSubscriber)
        {
            return _responseStream.Equals(topicSubscriber._responseStream);
        }

        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return _responseStream.GetHashCode();
    }
}
