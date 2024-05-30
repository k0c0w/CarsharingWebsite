using Microsoft.Extensions.DependencyInjection;

namespace ApiExtensions;

public static class ClientsServiceCollectionExtensions
{
    public static IServiceCollection AddBalanceServiceGrpcClients(this IServiceCollection services, string balanceMicroserviceAddress)
    {
        var uri = new Uri(balanceMicroserviceAddress);

        services.AddGrpcClient<BalanceMicroservice.Clients.BalanceService.BalanceServiceClient>(o =>
        {
            o.Address = uri;
        })
        .ConfigurePrimaryHttpMessageHandler(() =>
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            return handler;
        });

        services.AddGrpcClient<BalanceMicroservice.Clients.UserManagementService.UserManagementServiceClient>(o =>
        {
            o.Address = uri;
        })
        .ConfigurePrimaryHttpMessageHandler(() =>
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            return handler;
        });

        return services;
    }
}
