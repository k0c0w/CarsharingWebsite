namespace Persistence.Chat.ChatEntites.SignalRModels;

public class ChatUser
{
    public bool IsAnonymous { get; }

    public string UserId { get; }

    public string Name { get; }

    public List<string> UserConnections { get; } = new List<string>();

    public int ConnectionsCount => UserConnections.Count;

    public ChatUser(string userId, string? userName)
    {
        UserId = userId;
        IsAnonymous = userName == null;
        Name = userName ?? "anonymous";
    }

    public void AddConnection(string connection)
    {
        UserConnections.Add(connection);
    }

    public void RemoveConnection(string connection)
    {
        UserConnections.Remove(connection);
    }
}
