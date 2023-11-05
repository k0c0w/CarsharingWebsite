using AutoMapper;
using Domain.Entities;
using Features.CarManagement.Commands.CreateModel;
using Migrations.CarsharingApp;
using Shared.CQRS;
using Shared.Results;

namespace Features.CarManagement.Commands.CreateCar;

public class CreateCarCommandHandler : ICommandHandler<CreateModelCommand>
{
    private readonly CarsharingContext _ctx;
    private readonly IMapper _mapper;

    public CreateCarCommandHandler(CarsharingContext ctx, IMapper mapper)
    {
        _ctx = ctx;
        _mapper = mapper;
    }

    public async Task<Result> Handle(CreateModelCommand request, CancellationToken cancellationToken)
    {
        var car = _mapper.Map<Car>(request.CreateCarModelDto);
        await _ctx.Cars.AddAsync(car, cancellationToken);
        await _ctx.SaveChangesAsync(cancellationToken);
        return new Ok();
    }
}