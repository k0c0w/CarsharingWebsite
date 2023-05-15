
using Contracts.Tariff;

namespace Services.Abstractions;

public interface ITariffService
{
     Task<IEnumerable<TariffDto>> GetAllActiveAsync();

     Task<TariffDto> GetActiveTariffById(int id);
}