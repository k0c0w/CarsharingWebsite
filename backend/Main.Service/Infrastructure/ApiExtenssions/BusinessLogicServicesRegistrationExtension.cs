using Features.Users.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Services;
using Services;
using Persistence;

namespace ApiExtensions;

public static class BusinessLogicServicesRegistrationExtension
{
    public static IServiceCollection RegisterBusinessLogicServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepositories();
        services.AddUnitsOfWork();

        services.AddScoped<IBookCarService, BookCarService>();
        services.AddScoped<ISubscriptionService, SubscriptionService>();
        services.AddScoped<IBalanceService, BalanceService>();
        services.AddScoped<IUserBalanceCreatorService, UserBalanceCreator>();

        services.AddBalanceServiceGrpcClients(configuration["KnownHosts:BackendHosts:BalanceMicroservice"]!);

        services.AddScoped<UserValidation>();

        return services;
    }
}
