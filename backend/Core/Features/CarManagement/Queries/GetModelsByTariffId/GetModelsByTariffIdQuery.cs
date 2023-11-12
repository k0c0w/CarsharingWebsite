using Contracts;
using Shared.CQRS;

namespace Features.CarManagement;

public record GetModelsByTariffIdQuery(int TariffId) : IQuery<IEnumerable<CarModelDto>>;