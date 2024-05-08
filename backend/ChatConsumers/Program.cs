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
        cfg.ConfigureEndpoints(ctx);
        cfg.Host(builder.Configuration
            .GetSection(nameof(RabbitMqConfig))
            .Get<RabbitMqConfig>()!
            .FullHostname);
        Console.WriteLine(builder.Configuration
            .GetSection(nameof(RabbitMqConfig))
            .Get<RabbitMqConfig>()!
            .FullHostname);
    });
});

var app = builder.Build();

app.Run();