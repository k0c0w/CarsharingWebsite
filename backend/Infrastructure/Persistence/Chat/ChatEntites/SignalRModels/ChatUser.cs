namespace Persistence.Chat.ChatEntites.SignalRModels;

public class ChatUser
{
    public bool IsAnonymous { get; }

    public string UserId { get; }

    public string Name { get; }

    public ChatUser(string userId, bool isAnonymous, string userName)
    {
        UserId = userId;
        IsAnonymous = isAnonymous;
        Name = userName;
    }

    public List<string> UserConnections { get; } = new List<string>();

    public void AddConnection(string connection)
    {
        UserConnections.Add(connection);
    }

    public void RemoveConnection(string connection)
    {
        UserConnections.Remove(connection);
    }

    public List<string> UserGroups { get; } = new List<string>();

    public void AddGroup(string group)
    {
        UserGroups.Add(group);
    }

    public void RemoveGroup(string group)
    {
        UserGroups.Remove(group);
    }
}
