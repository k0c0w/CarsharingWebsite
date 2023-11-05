using Contracts;
using Shared.CQRS;

namespace Features.CarManagement.Queries.GetCarsByModel;

public record GetCarsByModelQuery(int ModelId) : IQuery<IEnumerable<CarDto>>;