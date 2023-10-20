using Persistence.Chat;
using Persistence;

namespace Carsharing.Helpers.Extensions.ServiceRegistration
{
    public static class ChatRegistration
    {
        public static IServiceCollection RegisterChat(this IServiceCollection services)
        {
            services.AddSignalR();
            services.AddSingleton<IChatRoomRepository, ChatRepository>();
            services.AddSingleton<IChatUserRepository, ChatRepository>();

            return services;
        }
    }
}
