﻿using Chat.Persistance;
using ChatService;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Services.Abstractions;
using Shared;
using System.Security.Claims;

namespace Chat.Services;

internal class ChatServiceImplementaion(IChatService chatService) : ChatService.ChatService.ChatServiceBase
{
    private const string MANAGER_ROLE = "Manager";

    private readonly IChatService _chatService = chatService;


    public override async Task<SendMessageResultMessage> SendMessage(FromClientMessage request, ServerCallContext context)
    {
        var httpContext = context.GetHttpContext();
        var currentUserId = httpContext.User.GetId();
        var requestedTopicIsPersistent = !request.TopicName.StartsWith("anonymous_");

        var chatInfo = new ChatInfoDto(request.TopicName, requestedTopicIsPersistent);
        var messageDto = new SendMessageDto(chatInfo, request.Text, currentUserId);

        var invalidTopicNameForUnauthorizedUser = !(IsAuthenticatedUser(httpContext.User) || requestedTopicIsPersistent);
        var userIsManager = httpContext.User.UserIsInRole(MANAGER_ROLE);
        var isValidTopicNameForAuthorizedUser = request.TopicName == currentUserId;
        var shouldDecline = invalidTopicNameForUnauthorizedUser || !(userIsManager || isValidTopicNameForAuthorizedUser);
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

        var topicName = IsAuthenticatedUser(httpContext.User) ? httpContext.User!.GetId() : $"anonymous_{httpContext.Connection.Id}";
        return GetStreamAsync(topicName, responseStream, context);
    }

    [Authorize(Roles = MANAGER_ROLE)]
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
                await Task.Delay(TimeSpan.FromDays(1), streamCancellationToken);
            }
            catch (TaskCanceledException)
            {
                break;
            }
        }

        await _chatService.RemoveFromChatAsync(chatName: topicName, subscriber: topicSubscriber);
    }

    private static bool IsAuthenticatedUser(ClaimsPrincipal? claimsPrincipal) 
        => claimsPrincipal != null && claimsPrincipal.Identity != null && claimsPrincipal.Identity.IsAuthenticated;
}
