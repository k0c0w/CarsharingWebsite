using Persistence.Chat.ChatEntites.SignalRModels;

namespace Carsharing.ChatHub;

public interface IChatClient
{
    Task RecieveMessage(ChatMessage message);
    Task JoinRoomResult(JoinRoomResult joinRoomResult);
    Task LeaveRoomResult(LeaveRoomResult leaveRoomResult);
    Task ChatRoomUpdate(ChatRoomUpdate chatRoomUpdate);
    Task SendAsync(string method, object message);
}