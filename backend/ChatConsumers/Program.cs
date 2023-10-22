using Microsoft.EntityFrameworkCore;
using Migrations.Chat;
using MassTransit;
using Carsharing.ChatHub;
using ChatConsumers.Options;
using Persistence.Chat;
using Persistence.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ChatContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<ChatMessageConsumer>();

    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.ConfigureEndpoints(ctx);
        cfg.Host(builder.Configuration
            .GetSection(nameof(RabbitMqConfig))
            .Get<RabbitMqConfig>()!
            .FullHostname);
    });
});
builder.Services.AddScoped<IMessageUnitOfWork, ChatUnitOfWork>();

var app = builder.Build();

app.Run();