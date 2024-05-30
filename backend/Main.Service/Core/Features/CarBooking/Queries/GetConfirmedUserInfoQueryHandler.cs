using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Migrations.CarsharingApp;
using Services.Exceptions;

namespace Features.CarBooking.Queries;

public class GetConfirmedUserInfoQueryHandler : IQueryHandler<GetConfirmedUserInfoQuery, UserInfo>
{
    private readonly CarsharingContext _ctx;

    public GetConfirmedUserInfoQueryHandler(CarsharingContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<Result<UserInfo>> Handle(GetConfirmedUserInfoQuery request, CancellationToken cancellationToken)
    {
        var user = await _ctx.Users.Include(x => x.UserInfo).FirstOrDefaultAsync( x=> x.Id== request.UserId, cancellationToken: cancellationToken);
        var userInfo = user!.UserInfo;
        
        if (userInfo == null) 
            return new Error<UserInfo>(new ObjectNotFoundException($"No such User with id:{request.UserId}").Message);
        if (!userInfo.Verified) 
            return new Error<UserInfo>(new InvalidOperationException("Profile is not confirmed").Message);
        
        return new Ok<UserInfo>(userInfo);
    }
}