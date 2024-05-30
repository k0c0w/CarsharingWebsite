using Chat.Helpers;
using ChatService;
using Domain.Repositories;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using CommonExtensions.Claims;

namespace Chat.GrpcImplementations;

internal class ManagementServiceGrpc(ITopicRepository topicRepository, IMessageRepository messageRepository) : ManagementService.ManagementServiceBase
{
    private readonly ITopicRepository _topicRepository = topicRepository;

    private readonly IMessageRepository _messageRepository = messageRepository;

    [Authorize(Roles = "Manager")]
    public override async Task<ActiveTopicsMessage> GetActiveTopics(Empty request, ServerCallContext context)
    {
        var topics = await _topicRepository.GetAllAsync();

        var response = new ActiveTopicsMessage();
        response.Topic.AddRange(topics.Select(x => x.Name));

        return response;
    }

    [Authorize]
    public override Task<ChatHistoryMessage> GetMyChatHistory(MyChatHistorySelectorMessage request, ServerCallContext context)
    {
        var httpContext = context.GetHttpContext();

        return GetChatHistoryReplyAsync(httpContext.User.GetId(), request.Limit ?? 128, request.Offset ?? 0);
    }

    [Authorize(Roles = "Manager")]
    public override Task<ChatHistoryMessage> GetChatHistory(ChatHistorySelectorMessage request, ServerCallContext context)
    {
        return GetChatHistoryReplyAsync(request.Topic, request.Limit ?? 128, request.Offset ?? 0);
    }

    private async Task<ChatHistoryMessage> GetChatHistoryReplyAsync(string topic, int limit, int offset)
    {
        var reply = new ChatHistoryMessage();

        var messages = await _messageRepository.GetMessagesByTopicAsync(topic, limit, offset);
        reply.History.AddRange(messages.Select(x => x.ToGrpcMessage()));

        return reply;
    }
}
