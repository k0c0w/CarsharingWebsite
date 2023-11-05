using Contracts;
using Shared.CQRS;

namespace Features.CarManagement.Queries.GetModelsByTariffId;

public record GetModelsByTariffIdQuery(int TariffId) : IQuery<IEnumerable<CarModelDto>>;