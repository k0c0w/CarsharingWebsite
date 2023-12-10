using Persistence.Chat.ChatEntites.SignalRModels;
using System.Collections.Concurrent;

namespace Persistence.Chat;

public class ChatRepository : IChatUserRepository<ChatUser>, IChatRoomRepository<TechSupportChatRoom>
{
    // to map user and their connections
    // for anonymous users we will use their session token (notice that it can be hacked)
    private static readonly ConcurrentDictionary<string, TechSupportChatRoom> Rooms = new();

    private static readonly ConcurrentDictionary<string, ChatUser> ConnectedUsers = new();


    public bool TryGetUser(string userId, out ChatUser? chatUser)
    {
        return ConnectedUsers.TryGetValue(userId, out chatUser);
    }

    public bool TryGetRoom(string roomId, out TechSupportChatRoom? chatRoom)
    {
        return Rooms.TryGetValue(roomId, out chatRoom);
    }

    public bool TryRemoveRoom(string roomId, out TechSupportChatRoom? chatRoom)
    {
        return Rooms.TryRemove(roomId, out chatRoom);
    }

    public bool TryAddRoom(string roomId, TechSupportChatRoom techSupportChatRoom)
    {
        return Rooms.TryAdd(roomId, techSupportChatRoom);
    }

    public bool TryRemoveUser(string userId, out ChatUser? chatUser)
    {
        return ConnectedUsers.TryRemove(userId, out chatUser);
    }

    public bool TryAddUser(string userId, ChatUser user)
    {
       return ConnectedUsers.TryAdd(userId, user);
    }

    public bool ContainsUserById(string userId)
    {
        return ConnectedUsers.ContainsKey(userId);
    }

    public IEnumerable<TechSupportChatRoom> GetAll()
    { 
        return Rooms.Values.ToArray();
    }
}
