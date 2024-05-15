using Chat.Helpers;
using ChatService;
using Domain;
using Domain.Interfaces;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace Chat.Persistance;

// can not put this into separate project due to circular referrences
internal class TopicSubscriber(string assignedTopic, IServerStreamWriter<FromServerMessage> responseStream) : ITopicSubscriber
{
    private readonly string _assignedTopic = assignedTopic;

    private readonly IServerStreamWriter<FromServerMessage> _responseStream = responseStream;

    public async Task ReceiveAsync(MessageAggregate messageAggregate, CancellationToken ct = default)
    {
        var sendMessage = new FromServerMessage()
        {
            Message = messageAggregate.ToGrpcMessage(),
        };

        await _responseStream.WriteAsync(sendMessage);
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
