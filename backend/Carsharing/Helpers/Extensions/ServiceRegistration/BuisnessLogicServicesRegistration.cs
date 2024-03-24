using Clients.DependecyInjectionExtensions;
using Features.Users.Shared;
using Persistence;
using Persistence.Services;
using Services;

namespace Carsharing.Helpers.Extensions.ServiceRegistration;

public static class BuisnessLogicServicesRegistrationExtension
{
    public static IServiceCollection RegisterBuisnessLogicServices(this IServiceCollection services, IConfiguration configuration)
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
