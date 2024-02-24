using Contracts;
using Shared.CQRS;

namespace Features.CarManagement;

public record GetAllModelsQuery() : IQuery<IEnumerable<CarModelDto>>;