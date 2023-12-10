using System.Collections.Concurrent;
using Persistence.Chat.ChatEntites.SignalRModels;

namespace Persistence.Chat.ChatEntites;

public class OccasionChatRepository
{
    // to map user and their connections
    // for anonymous users we will use their session token (notice that it can be hacked)
    private static readonly ConcurrentDictionary<Guid, OccasionsSupportChatRoom> Rooms = new();

    private static readonly ConcurrentDictionary<string, OccasionChatUser> ConnectedUsers = new();

    public bool TryGetUser(string userId, out OccasionChatUser? chatUser)
    {
        return ConnectedUsers.TryGetValue(userId, out chatUser);
    }

    public bool TryGetRoom(Guid roomId, out OccasionsSupportChatRoom? chatRoom)
    {
        return Rooms.TryGetValue(roomId, out chatRoom);
    }

    public bool TryGetRoomByUser(string userId, out OccasionsSupportChatRoom chatRoom)
    {
        foreach (var pair in Rooms)
        {
            if (pair.Value.Client.UserId == userId)
            {
                chatRoom = pair.Value;
                return true;
            }
        }

        chatRoom = null;
        return false;
    }
    
    public bool TryRemoveRoom(Guid roomId, out OccasionsSupportChatRoom? chatRoom)
    {
        return Rooms.TryRemove(roomId, out chatRoom);
    }
    
    public bool TryRemoveRoomByUserId(string userId, out OccasionsSupportChatRoom? chatRoom)
    {
        foreach (var pair in Rooms)
        {
            if (pair.Value.Client.UserId == userId)
            {
                TryRemoveRoom(pair.Value.RoomId, out chatRoom);
                return true;
            }
        }

        chatRoom = null;
        return false;
    }

    public bool TryAddRoom(Guid roomId, OccasionsSupportChatRoom techSupportChatRoom)
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
