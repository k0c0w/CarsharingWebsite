using Carsharing.ChatHub;
using Domain.Common;
using Persistence.Chat.ChatEntites;

namespace Carsharing.Helpers.Extensions.ServiceRegistration;

public static class ChatRegistration
{
    public static IServiceCollection RegisterChat(this IServiceCollection services)
    {
        services.AddSignalR();
        services.AddSingleton<OccasionChatRepository>();

        services.AddTransient<IMessageProducer, MessageProducer>();

        return services;
    }
}
