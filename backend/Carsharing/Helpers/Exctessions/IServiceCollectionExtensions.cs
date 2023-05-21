using Services;
using Services.Abstractions;
using Services.Abstractions.Admin;

namespace Carsharing;

public static class IServiceCollectionExtensions 
{
    public static void AddTariffService(this IServiceCollection services)
    {
        services.AddScoped<ITariffService, TariffService>();
        services.AddScoped<IAdminTariffService, TariffService>();
    }
}