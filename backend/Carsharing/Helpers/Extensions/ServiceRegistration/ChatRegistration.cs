using Persistence.Chat;
using Persistence;
using MassTransit;
using Carsharing.ChatHub;
using Domain.Common;

namespace Carsharing.Helpers.Extensions.ServiceRegistration
{
    public static class ChatRegistration
    {
        public static IServiceCollection RegisterChat(this IServiceCollection services)
        {
            services.AddSignalR();
            services.AddSingleton<IChatRoomRepository, ChatRepository>();
            services.AddSingleton<IChatUserRepository, ChatRepository>();

            services.AddTransient<IMessageProducer, MessageProducer>();

            return services;
        }
    }
}
