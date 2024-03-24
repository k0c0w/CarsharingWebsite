using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Migrations.CarsharingApp;
using Shared.CQRS;
using Shared.Results;
using static BalanceMicroservice.Clients.UserManagementService;

namespace Features.Users.Queries.GetUserWithInfo;

public class GetUserWithInfoQueryHandler : IQueryHandler<GetUserWithInfoQuery, User>
{
    private readonly CarsharingContext _context;
    private readonly UserManagementServiceClient _userManagementServiceClient;

    public GetUserWithInfoQueryHandler(CarsharingContext context, UserManagementServiceClient userManagementServiceClient)
    {
        _context = context;
        _userManagementServiceClient = userManagementServiceClient;
    }

    public async Task<Result<User>> Handle(GetUserWithInfoQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(x => x.UserInfo).FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken: cancellationToken);
        if (user == null) return new Error<User>("Not found");

        var balance = (await _userManagementServiceClient.GetUserInfoAsync(new BalanceMicroservice.Clients.GrpcUserRequest() { Id = user!.Id })).Balance;

        user.UserInfo.Balance = balance.IntegerPart + balance.FractionPart / 100m;

        return new Ok<User>(user);
    }
}