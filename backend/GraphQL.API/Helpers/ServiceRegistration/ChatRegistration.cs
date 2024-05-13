using Carsharing.ChatHub;
using Domain.Common;
using Persistence;
using Persistence.Chat;
using Persistence.Chat.ChatEntites;
using Persistence.Chat.ChatEntites.SignalRModels;

namespace GraphQL.API.Helpers.ServiceRegistration;

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
