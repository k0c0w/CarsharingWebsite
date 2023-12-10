using Persistence.Chat.ChatEntites.SignalRModels;
using Persistence.Chat.ChatEntites.SignalRModels.Shared;

namespace Carsharing.ChatHub;

public interface IOccasionChatClient
{
    Task RecieveMessage(OccasionChatMessage message);
    Task JoinRoomResult(JoinRoomResult joinRoomResult);
    Task LeaveRoomResult(LeaveRoomResult leaveRoomResult);
    Task ChatRoomUpdate(ChatRoomUpdate chatRoomUpdate);
    Task RecieveRoomId(string roomId);
}