using ChatService;
using Domain;
using Google.Protobuf.WellKnownTypes;

namespace Chat.Helpers;

public static class MappingExtensions
{
    internal static ChatService.Message ToGrpcMessage(this MessageAggregate messageAggregate)
    {
        var message = messageAggregate.Message;
        var author = messageAggregate.Author;

        return new ChatService.Message
        {
            Id = message.Id.ToString(),
            Text = message.Text,
            Time = Timestamp.FromDateTimeOffset(message.Time),
            Author = new MessageAuthor
            {
                Id = author.Id,
                IsManager = author.IsManager,
                Name = author.Name,
            },
        };
    }
}
