namespace Persistence.Chat.ChatEntites.SignalRModels.Shared;

public abstract class ChatUserBase
{

    public string UserId { get; }

    public string Name { get; }
    
    public int ConnectionsCount => UserConnections.Count;

    public List<string> UserConnections { get; } = new List<string>();
    

    public ChatUserBase(string userId, string? userName)
    {
        UserId = userId;
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