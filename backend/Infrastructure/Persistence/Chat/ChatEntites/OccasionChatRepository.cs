using System.Collections.Concurrent;
using Persistence.Chat.ChatEntites.SignalRModels;

namespace Persistence.Chat.ChatEntites;

public class OccasionChatRepository
{
    // to map user and their connections
    // for anonymous users we will use their session token (notice that it can be hacked)
    private static readonly ConcurrentDictionary<Guid, OccasionChatRoom> Rooms = new();

    private static readonly ConcurrentDictionary<string, OccasionChatUser> ConnectedUsers = new();

    // admin connection to connectiongroup
    private static readonly ConcurrentDictionary<string, string> AdminConnectionsToGroups= new();

    public bool TryGetUser(string userId, out OccasionChatUser? chatUser)
    {
        return ConnectedUsers.TryGetValue(userId, out chatUser);
    }

    public bool TryRemoveRoom(Guid roomId)
    {
        return Rooms.TryRemove(roomId, out _);
    }

    public bool TryAddRoom(Guid occasionId, Guid issuerId)
    {
        var room = new OccasionChatRoom(issuerId, occasionId, occasionId);

        return Rooms.TryAdd(occasionId, room);
    }

    public bool TryAddAdminConnectionToGroup(string connectionId, string groupId)
    {
        if (AdminConnectionsToGroups.TryGetValue(connectionId, out _))
            return false;

        return AdminConnectionsToGroups.TryAdd(connectionId, groupId);
    }

    public bool TryRemoveAdminConnection(string connectionId, out string groupname)
    {
        return AdminConnectionsToGroups.TryRemove(connectionId, out groupname);
    }

    public bool TryGetAdminConnectionGroup(string connectionId, out string groupname) => AdminConnectionsToGroups.TryGetValue(connectionId, out groupname);

    public bool TryRemoveUser(string userId, out OccasionChatUser? chatUser)
    {
        return ConnectedUsers.TryRemove(userId, out chatUser);
    }

    public bool TryAddUser(string userId, OccasionChatUser user)
    {
        return ConnectedUsers.TryAdd(userId, user);
    }

    public IEnumerable<OccasionChatRoom> GetAll()
    { 
        return Rooms.Values.ToArray();
    }
}
