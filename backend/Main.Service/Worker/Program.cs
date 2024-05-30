using Microsoft.EntityFrameworkCore;
using MassTransit;
using ChatConsumers.Options;
using ChatConsumers;
using Persistence.RepositoryImplementation;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Main.Service.Worker.HostedServices;
using Migrations.CarsharingApp;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddTransient<OccasionMessageRepository>();
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<OccasionMessageConsumer>();

    config.UsingRabbitMq((ctx, cfg) =>
    {
        var config = builder.Configuration
            .GetSection(nameof(RabbitMqConfig))
            .Get<RabbitMqConfig>() ?? throw new ArgumentNullException("Provide RabbitMQ config");

        cfg.ConfigureEndpoints(ctx);
        cfg.Host(config.Host, hostCfg =>
        {
            hostCfg.Username(config.Username);
            hostCfg.Password(config.Password);
        });
    });
});

var migrationAssemblyName = typeof(CarsharingContext).Assembly.ToString();

builder.Services.AddDbContext<CarsharingContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
        x =>
        {
            x.MigrationsAssembly(migrationAssemblyName);
            x.MinBatchSize(1);
        });
});

builder.Services.AddHostedService<SubscriptionChecker>();

var app = builder.Build();

await app.RunAsync();