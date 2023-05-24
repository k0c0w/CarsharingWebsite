using Contracts.Tariff;

namespace Services.Abstractions.Admin;

public interface IAdminTariffService : ITariffService
{
    Task CreateAsync(CreateTariffDto create);

    Task EditAsync(int id, CreateTariffDto update);
    
    Task TurnOnAsync(int id);

    Task TurnOffAsync(int id);

    Task DeleteAsync(int id);

    Task<AdminTariffDto> GetTariffByIdAsync(int id);

    Task<IEnumerable<AdminTariffDto>> GetAllAsync();
}