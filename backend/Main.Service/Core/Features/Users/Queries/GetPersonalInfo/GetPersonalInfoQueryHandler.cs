using AutoMapper;
using Contracts.User;
using Features.Users.Queries.GetUserWithInfo;
using MediatR;

namespace Features.Users.Queries.GetPersonalInfo;

public class GetPersonalInfoQueryHandler : IQueryHandler<GetPersonalInfoQuery, UserInfoDto>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetPersonalInfoQueryHandler(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<Result<UserInfoDto>> Handle(GetPersonalInfoQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetUserWithInfoQuery(request.UserId), cancellationToken);
        return result.IsSuccess 
            ? new Ok<UserInfoDto>(_mapper.Map<UserInfoDto>(result.Value)) 
            : new Error<UserInfoDto>(result.ErrorMessage);
    }
}