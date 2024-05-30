using Contracts;
namespace Features.CarManagement;

public record GetModelsByTariffIdQuery(int TariffId) : IQuery<IEnumerable<CarModelDto>>;