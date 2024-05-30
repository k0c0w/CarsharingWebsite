using Contracts;

namespace Features.CarManagement;

public record GetAvailableCarsByModelQuery(int ModelId) : IQuery<IEnumerable<CarDto>>;