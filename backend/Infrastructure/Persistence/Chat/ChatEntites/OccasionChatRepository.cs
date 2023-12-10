using System.Collections.Concurrent;
using Persistence.Chat.ChatEntites.SignalRModels;

namespace Persistence.Chat.ChatEntites;

public class OccasionChatRepository : IChatUserRepository<OccasionChatUser>, IChatRoomRepository<OccasionsSupportChatRoom>
{
    // to map user and their connections
    // for anonymous users we will use their session token (notice that it can be hacked)
    private static readonly ConcurrentDictionary<string, OccasionsSupportChatRoom> Rooms = new();

    private static readonly ConcurrentDictionary<string, OccasionChatUser> ConnectedUsers = new();


    public bool TryGetUser(string userId, out OccasionChatUser? chatUser)
    {
        return ConnectedUsers.TryGetValue(userId, out chatUser);
    }

    public bool TryGetRoom(string roomId, out OccasionsSupportChatRoom? chatRoom)
    {
        return Rooms.TryGetValue(roomId, out chatRoom);
    }

    public bool TryRemoveRoom(string roomId, out OccasionsSupportChatRoom? chatRoom)
    {
        return Rooms.TryRemove(roomId, out chatRoom);
    }

    public bool TryAddRoom(string roomId, OccasionsSupportChatRoom techSupportChatRoom)
    {
        return Rooms.TryAdd(roomId, techSupportChatRoom);
    }

    public bool TryRemoveUser(string userId, out OccasionChatUser? chatUser)
    {
        return ConnectedUsers.TryRemove(userId, out chatUser);
    }

    public bool TryAddUser(string userId, OccasionChatUser user)
    {
        return ConnectedUsers.TryAdd(userId, user);
    }

    public bool ContainsUserById(string userId)
    {
        return ConnectedUsers.ContainsKey(userId);
    }

    public IEnumerable<OccasionsSupportChatRoom> GetAll()
    { 
        return Rooms.Values.ToArray();
    }
}
