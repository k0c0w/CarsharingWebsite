using Persistence.Chat.ChatEntites.SignalRModels;
using Persistence.Chat.ChatEntites.SignalRModels.Shared;

namespace Persistence;

public interface IChatRoomRepository<TChatRoom> where TChatRoom: ChatRoom
{
    public bool TryGetRoom(string roomId, out TChatRoom? chatRoom);

    public bool TryRemoveRoom(string roomId, out TChatRoom? chatRoom);

    public bool TryAddRoom(string roomId, TChatRoom techSupportChatRoom);

    public IEnumerable<TChatRoom> GetAll();
}
