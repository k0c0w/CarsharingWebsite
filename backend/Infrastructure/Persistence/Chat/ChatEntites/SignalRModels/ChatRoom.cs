namespace Persistence.Chat.ChatEntites.SignalRModels;

public class ChatRoom
{
    public List<ChatUser> Users { get; set; } = new List<ChatUser>();

    public string InitializerUserId { get; set; } = string.Empty;
}
