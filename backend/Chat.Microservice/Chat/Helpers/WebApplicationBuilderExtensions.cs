using Chat.Helpers.Options;
using Domain.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Migrations;
using Persistence.RepositoriesImplementations;
using Persistence.ServicesImplementations;
using Services.Abstractions;

namespace Chat.Helpers;

public static class WebApplicationBuilderExtensions
{
    public static void Configure(this WebApplicationBuilder builder)
    {
        var currentAssembly = typeof(Program).Assembly;
        var services = builder.Services;
        var configuration = builder.Configuration;

        services
            .AddAuthorization(configuration)
            .AddGrpc();

        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<ITopicRepository, TopicRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddDbContext<ChatServiceContext>(options => 
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), x =>
            {
                x.EnableRetryOnFailure(5);
            });
        });

        RabbitMqConfig rabbitConfig = configuration.GetSection(nameof(RabbitMqConfig))?.Get<RabbitMqConfig>() ?? throw new ArgumentNullException($"Provide {nameof(RabbitMqConfig)}");
        services.AddMassTransit(config =>
        {
            config.AddEntityFrameworkOutbox<ChatServiceContext>(cfg =>
            {
                cfg.DuplicateDetectionWindow = TimeSpan.FromMinutes(1);
            });

            config.AddConsumers(currentAssembly);
            config.AddActivities(currentAssembly);

            config.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.ConfigureEndpoints(ctx);
                cfg.Host(rabbitConfig.Host, hostCfg =>
                {
                    hostCfg.Password(rabbitConfig.Password);
                    hostCfg.Username(rabbitConfig.Username);
                });
            });
        });

        services.AddScoped<IChatService, Persistence.Services.Implementations.ChatService>();
        services.AddScoped<IUserService, UserService>();
    }
}
