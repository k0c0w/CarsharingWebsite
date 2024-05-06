using ChatService;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;

namespace Chat.Services;

[Authorize]
internal class ChatServiceImplementaion : ChatService.ChatService.ChatServiceBase
{
    public override Task GetChatStream(Empty request, IServerStreamWriter<FromServerMessage> responseStream, ServerCallContext context)
    {
        return base.GetChatStream(request, responseStream, context);
    }

    public override Task<Empty> SendMessage(FromClientMessage request, ServerCallContext context)
    {
        return base.SendMessage(request, context);
    }
}
