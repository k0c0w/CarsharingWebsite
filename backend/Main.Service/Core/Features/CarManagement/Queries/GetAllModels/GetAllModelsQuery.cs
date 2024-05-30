using Contracts;

namespace Features.CarManagement;

public record GetAllModelsQuery() : IQuery<IEnumerable<CarModelDto>>;