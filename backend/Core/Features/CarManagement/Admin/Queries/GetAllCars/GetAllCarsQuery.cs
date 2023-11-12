using Contracts;
using Shared.CQRS;

namespace Features.CarManagement.Admin;

public sealed record GetAllCarsQuery() : IQuery<IEnumerable<CarDto>>;