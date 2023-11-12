using Services.Abstractions.Admin;
using Services.Abstractions;
using Services;
using Carsharing.Services;

namespace Carsharing.Helpers.Extensions.ServiceRegistration;

public static class BuisnessLogicServicesRegistrationExtension
{

    public static IServiceCollection RegisterBuisnessLogicServices(this IServiceCollection services)
    {
        services.AddRepositories();
        services.AddUnitsOfWork();

        services.AddScoped<ITariffService, TariffService>();
        services.AddScoped<IAdminTariffService, TariffService>();
        services.AddScoped<IFileProvider, FileProvider>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IBalanceService, BalanceService>();

        return services;
    }
}
