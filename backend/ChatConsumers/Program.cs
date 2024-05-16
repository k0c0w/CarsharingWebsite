using Microsoft.EntityFrameworkCore;
using MassTransit;
using ChatConsumers.Options;
using ChatConsumers;
using Persistence.RepositoryImplementation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<OccasionMessageRepository>();
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<OccasionMessageConsumer>();

    config.UsingRabbitMq((ctx, cfg) =>
    {
        var config = builder.Configuration
            .GetSection(nameof(RabbitMqConfig))
            .Get<RabbitMqConfig>() ?? throw new ArgumentNullException("Provide Rabbitmq config");

        cfg.ConfigureEndpoints(ctx);
        cfg.Host(config.Host, hostCfg =>
        {
            hostCfg.Username(config.Username);
            hostCfg.Password(config.Password);
        });
    });
});

var app = builder.Build();

app.Run();