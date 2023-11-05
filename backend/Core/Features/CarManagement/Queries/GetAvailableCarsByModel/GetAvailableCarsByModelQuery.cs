using Contracts;
using Shared.CQRS;

namespace Features.CarManagement.Queries.GetAvailableCarsByModel;

public record GetAvailableCarsByModelQuery(int ModelId) : IQuery<IEnumerable<CarDto>>;