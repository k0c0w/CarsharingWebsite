using AutoMapper;
using Contracts;
using Contracts.User;
using Domain.Entities;
using Features.Users.Queries.GetPersonalInfo;
using Features.Users.Queries.GetUserWithInfo;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Migrations.CarsharingApp;
using Shared.CQRS;
using Shared.Results;

namespace Features.Users.Queries.GetProfileInfo;

public class GetProfileInfoQueryHandler : IQueryHandler<GetProfileInfoQuery, ProfileInfoDto>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly CarsharingContext _context;

    public GetProfileInfoQueryHandler(IMediator mediator, IMapper mapper, CarsharingContext context)
    {
        _mediator = mediator;
        _mapper = mapper;
        _context = context;
    }

    public async Task<Result<ProfileInfoDto>> Handle(GetProfileInfoQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetUserWithInfoQuery(request.UserId), cancellationToken);
        if (result.IsSuccess is not true)
            return new Error<ProfileInfoDto>(result.ErrorMessage);
        
        //TODO: сделать маппинг
        var userSubscriptions = await _context.Subscriptions
            .Where(x => x.IsActive)
            .Where(x => x.UserId == request.UserId)
            .Include( x=> x.Car!)
            .ThenInclude(x => x.CarModel)
            .ToListAsync(cancellationToken: cancellationToken);
        var bookedCars = userSubscriptions
            .Where(x => !x.IsExpired)   
            .OrderByDescending(x => x.EndDate)
            .Select(x => x.Car)
            .Select(x => new CarShortcutDto
            {
                Brand = x!.CarModel!.Brand,
                Model = x!.CarModel.Model,
                Id = x!.Id,
                IsOpened = x!.IsOpened,
                LicensePlate = x!.LicensePlate,
                ImageUrl = x!.CarModel.ImageUrl
            })
            .ToArray();

        return new Ok<ProfileInfoDto>(new ProfileInfoDto
        {
            PersonalInfo = _mapper.Map<User, UserInfoDto>(result!.Value!),
            CurrentlyBookedCars = bookedCars
        });
    }
}