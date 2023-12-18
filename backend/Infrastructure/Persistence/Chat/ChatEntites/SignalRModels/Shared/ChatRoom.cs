namespace Persistence.Chat.ChatEntites.SignalRModels.Shared;

public abstract class ChatRoom
{
    private readonly List<string> _managersIds = new List<string>();
    public IReadOnlyList<string> ProcessingManagersIds => _managersIds;
    public int ProcessingManagersCount => _managersIds.Count;
    public ChatUserBase Client { get; }

    protected ChatRoom(ChatUserBase client)
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