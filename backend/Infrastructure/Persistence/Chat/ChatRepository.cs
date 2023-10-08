using Persistence.Chat.ChatEntites.SignalRModels;
using System.Collections.Concurrent;


namespace Persistence.Chat;

public class ChatRepository : IChatUserRepository, IChatRoomRepository
{
    // to map user and their connections
    // for anonymous users we will use their session token (notice that it can be hacked)
    private static readonly ConcurrentDictionary<string, ChatRoom> Rooms = new ConcurrentDictionary<string, ChatRoom>();

    private static readonly ConcurrentDictionary<string, ChatUser> ConnectedUsers = new ConcurrentDictionary<string, ChatUser>();



    public bool TryGetUser(string chatUserId, out ChatUser? chatUser)
    {
        return ConnectedUsers.TryGetValue(chatUserId, out chatUser);
    }

    public bool TryGetRoom(string roomId, out ChatRoom? chatRoom)
    {
        return Rooms.TryGetValue(roomId, out chatRoom);
    }

    public bool TryRemoveRoom(string roomId, out ChatRoom? chatRoom)
    {
        return Rooms.TryRemove(roomId, out chatRoom);
    }

    public bool TryAddRoom(string roomId, ChatRoom chatRoom)
    {
        return Rooms.TryAdd(roomId, chatRoom);
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
}
