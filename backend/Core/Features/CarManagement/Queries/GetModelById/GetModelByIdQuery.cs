using Contracts;
using Shared.CQRS;

namespace Features.CarManagement.Queries.GetModelById;

public record GetModelByIdQuery(int Id) : IQuery<ExtendedCarModelDto>;