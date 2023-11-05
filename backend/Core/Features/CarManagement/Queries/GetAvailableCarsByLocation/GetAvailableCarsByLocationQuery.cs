using Contracts;
using Shared.CQRS;

namespace Features.CarManagement.Queries.GetAvailableCarsByLocation;

public record GetAvailableCarsByLocationQuery (
    SearchCarDto SearchParams,
    int Limit=256
    ) : IQuery<IEnumerable<FreeCarDto>>;