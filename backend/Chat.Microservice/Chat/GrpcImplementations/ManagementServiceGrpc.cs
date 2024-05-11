using Chat.Helpers;
using ChatService;
using Domain;
using Domain.Repositories;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Shared;

namespace Chat.GrpcImplementations;

internal class ManagementServiceGrpc(ITopicRepository topicRepository, IMessageRepository messageRepository) : ManagementService.ManagementServiceBase
{
    private readonly ITopicRepository _topicRepository = topicRepository;

    private readonly IMessageRepository _messageRepository = messageRepository;

    [Authorize(Roles = "Manager")]
    public override async Task<ActiveTopicsMessage> GetActiveTopics(GetActiveTopicsRequest request, ServerCallContext context)
    {
        var topics = await _topicRepository.GetAllAsync();

        var response = new ActiveTopicsMessage();
        response.Topic.AddRange(topics.Select(x => x.Name));

        return response;
    }

    [Authorize]
    public override async Task<ChatHistoryMessage> GetChatHistory(ChatHistorySelectorMessage request, ServerCallContext context)
    {
        var httpContext = context.GetHttpContext();
        var reply = new ChatHistoryMessage();

        if (httpContext.User.HasManagerRole() || request.Topic == httpContext.User.GetId())
        {
            var messages = await _messageRepository.GetMessagesByTopicAsync(request.Topic, request.Limit ?? 128, request.Offset ?? 0);

            reply.History.AddRange(messages.Select(x =>
            {
                var message = x.Message;
                var author = x.Author;

                return new ChatService.Message
                {
                    Id = message.Id.ToString(),
                    Text = message.Text,
                    Time = message.Time.ToTimestamp(),
                    Author = new MessageAuthor
                    {
                        Id = author.Id,
                        IsManager = author.IsManager,
                        Name = author.Name,
                    },
               };
            }));
        }

        return reply;
    }
}
