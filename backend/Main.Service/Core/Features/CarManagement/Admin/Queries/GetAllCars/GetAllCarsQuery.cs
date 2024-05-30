using Contracts;

namespace Features.CarManagement.Admin;

public sealed record GetAllCarsQuery() : IQuery<IEnumerable<CarDto>>;