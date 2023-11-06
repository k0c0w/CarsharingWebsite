using Contracts;
using Shared.CQRS;

namespace Features.CarManagement;

public record GetModelByIdQuery(int Id) : IQuery<ExtendedCarModelDto>;