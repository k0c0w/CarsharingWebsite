using Persistence.Chat;
using Persistence;
using MassTransit;
using Carsharing.Consumers;

namespace Carsharing.Helpers.Extensions.ServiceRegistration
{
    public static class ChatRegistration
    {
        public static IServiceCollection RegisterChat(this IServiceCollection services)
        {
            services.AddSignalR();
            services.AddSingleton<IChatRoomRepository, ChatRepository>();
            services.AddSingleton<IChatUserRepository, ChatRepository>();
            services.AddMassTransit(options =>
            {
                options.AddConsumer<ChatMessageConsumer>();
                options.UsingInMemory((context, configuration) =>
                {
                    configuration.ConfigureEndpoints(context);
                });
            });

            return services;
        }
    }
}
