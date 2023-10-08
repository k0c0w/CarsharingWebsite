namespace Persistence.Chat.ChatEntites.SignalRModels;

public class ChatRoom
{
    private readonly List<string> _managersIds = new List<string>();
    public IReadOnlyList<string> ProcessingManagersIds => _managersIds;

    public int ProcessingManagersCount => _managersIds.Count;

    public ChatUser Client { get; }

    public string RoomId => Client.UserId;

    public ChatRoom(ChatUser client)
    {
        Client = client;
    }

    public void AssignManager(string managerId)
    {
        _managersIds.Add(managerId);
    }

    public void RevokeManager(string managerId)
    {
        _managersIds.Remove(managerId);
    }
}
