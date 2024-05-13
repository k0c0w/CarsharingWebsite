using Chat.Helpers;
using Chat.Persistance;
using ChatService;
using Domain;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Services.Abstractions;
using Shared;

namespace Chat.Services;

internal class ChatServiceGrpc(IChatService chatService) : MessagingService.MessagingServiceBase
{
    private readonly IChatService _chatService = chatService;

    public override async Task<SendMessageResultMessage> SendMessage(FromClientMessage request, ServerCallContext context)
    {
        var httpContext = context.GetHttpContext();
        var currentUserId = httpContext.User.GetId();
        var requestedTopicIsPersistent = !request.TopicName.StartsWith("anonymous_");

        var chatInfo = new ChatInfoDto(request.TopicName, requestedTopicIsPersistent);
        var messageDto = new SendMessageDto(chatInfo, request.Text, currentUserId);

        var invalidTopicNameForUnauthorizedUser = !(httpContext.User.IsAuthenticatedUser() || requestedTopicIsPersistent);
        var isValidTopicNameForAuthorizedUser = request.TopicName == currentUserId;
        var shouldDecline = invalidTopicNameForUnauthorizedUser || !(httpContext.User.HasManagerRole() || isValidTopicNameForAuthorizedUser);
        // decline unauthorized message, but still unauthorized users can send messages in other unauthorized chats
        if (shouldDecline)
        {
            return new SendMessageResultMessage
            {
                Accepted = false,
            };
        }

        var accepted = await _chatService.ReceiveMessageAsync(messageDto);

        return new SendMessageResultMessage
        {
            Accepted = accepted
        };
    }

    public override Task GetChatStream(Empty request, IServerStreamWriter<FromServerMessage> responseStream, ServerCallContext context)
    {
        var httpContext = context.GetHttpContext();

        var topicName = httpContext.User.IsAuthenticatedUser() ? httpContext.User!.GetId() : $"anonymous_{httpContext.Connection.Id}";
        return GetStreamAsync(topicName, responseStream, context);
    }

    [Authorize(Roles = "Manager")]
    public override Task GetChatStreamByTopic(ChatSelectorMessage request, IServerStreamWriter<FromServerMessage> responseStream, ServerCallContext context)
    {
        return GetStreamAsync(request.Topic, responseStream, context);
    }

    private async Task GetStreamAsync(string topicName, IServerStreamWriter<FromServerMessage> responseStream, ServerCallContext context)
    {
        var streamCancellationToken = context.CancellationToken;
        var topicSubscriber = new TopicSubscriber(topicName, responseStream);

        await topicSubscriber.NotifyAssignedTopicAsync(streamCancellationToken);
        await _chatService.AddToChatAsync(chatName: topicName, subscriber: topicSubscriber);
        
        while (!streamCancellationToken.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(TimeSpan.FromHours(1), streamCancellationToken);
            }
            catch (TaskCanceledException)
            {
                break;
            }
        }

        await _chatService.RemoveFromChatAsync(chatName: topicName, subscriber: topicSubscriber);
    }
}
