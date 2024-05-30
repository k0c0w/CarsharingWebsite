using Contracts;

namespace Features.CarManagement;

public record GetModelByIdQuery(int Id) : IQuery<ExtendedCarModelDto>;