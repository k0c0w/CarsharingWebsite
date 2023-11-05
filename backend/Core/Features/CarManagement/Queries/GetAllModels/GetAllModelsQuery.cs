using Contracts;
using Shared.CQRS;

namespace Features.CarManagement.Queries.GetAllModels;

public record GetAllModelsQuery() : IQuery<IEnumerable<CarModelDto>>;