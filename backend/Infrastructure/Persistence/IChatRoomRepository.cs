using Persistence.Chat.ChatEntites.SignalRModels;

namespace Persistence;

public interface IChatRoomRepository
{
    public bool TryGetRoom(string roomId, out ChatRoom? chatRoom);

    public bool TryRemoveRoom(string roomId, out ChatRoom? chatRoom);

    public bool TryAddRoom(string roomId, ChatRoom chatRoom);
}
