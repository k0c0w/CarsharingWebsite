using Contracts;

namespace Features.CarManagement.Admin;

public record GetCarsByModelQuery(int ModelId) : IQuery<IEnumerable<CarDto>>;