using Persistence.Chat;
using Persistence;
using Carsharing.ChatHub;
using Domain.Common;
using Persistence.Chat.ChatEntites;
using Persistence.Chat.ChatEntites.SignalRModels;

namespace Carsharing.Helpers.Extensions.ServiceRegistration;

public static class ChatRegistration
{
    public static IServiceCollection RegisterChat(this IServiceCollection services)
    {
        services.AddSignalR();
        services.AddSingleton<IChatRoomRepository<TechSupportChatRoom>, ChatRepository>();
        services.AddSingleton<IChatUserRepository<ChatUser>, ChatRepository>();
        services.AddSingleton<OccasionChatRepository>();

        services.AddTransient<IMessageProducer, MessageProducer>();

        return services;
    }
}
