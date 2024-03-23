using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clients.DependecyInjectionExtensions;

public static class ClientsServiceCollectionExtensions
{
    public static IServiceCollection AddBalanceServiceClients(this IServiceCollection services, string balanceMicroserviceAddress)
    {
        var uri = new Uri(balanceMicroserviceAddress);

        services.AddGrpcClient<BalanceMicroservice.Clients.BalanceService.BalanceServiceClient>(o =>
        {
            o.Address = uri;
        });

        services.AddGrpcClient<BalanceMicroservice.Clients.UserManagementService.UserManagementServiceClient>(o =>
        {
            o.Address = uri;
        });

        return services;
    }

    public static IServiceCollection AddS3Client(this IServiceCollection services, IConfiguration configuration)
    {

        return services;
    }
}