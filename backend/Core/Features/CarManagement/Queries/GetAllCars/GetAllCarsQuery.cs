using Contracts;
using Shared.CQRS;

namespace Features.CarManagement.Queries.GetAllCars;

public sealed record GetAllCarsQuery() : IQuery<IEnumerable<CarDto>>;