using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clients.DependecyInjectionExtensions;

public static class ClientsServiceCollectionExtensions
{
    public static IServiceCollection AddBalanceServiceGrpcClients(this IServiceCollection services, string balanceMicroserviceAddress)
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
        throw new NotImplementedException("Implement registry of s3 client");
    }
}