using Contracts;
using Shared.CQRS;

namespace Features.CarManagement;

public record GetAvailableCarsByModelQuery(int ModelId) : IQuery<IEnumerable<CarDto>>;