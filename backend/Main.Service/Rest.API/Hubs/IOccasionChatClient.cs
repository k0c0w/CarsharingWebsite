using Persistence.Chat.ChatEntites.SignalRModels.Shared;

namespace Carsharing.ChatHub;

public interface IOccasionChatClient
{
    Task ReceiveMessage(OccasionChatMessage message);

    Task OccassionClosed(Guid occasionId);
}