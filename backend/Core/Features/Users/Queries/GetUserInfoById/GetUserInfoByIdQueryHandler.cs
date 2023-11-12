using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Migrations.CarsharingApp;
using Shared.CQRS;
using Shared.Results;

namespace Features.Users.Queries.GetUserInfoById;

public class GetUserInfoByIdQueryHandler : IQueryHandler<GetUserInfoByIdQuery, UserInfo>
{
    private readonly CarsharingContext _context;

    public GetUserInfoByIdQueryHandler(CarsharingContext context)
    {
        _context = context;
    }

    public async Task<Result<UserInfo>> Handle(GetUserInfoByIdQuery request, CancellationToken cancellationToken)
    {
        return new Ok<UserInfo>(await _context.UserInfos
            .AsNoTracking()
            .Include(u => u.User)
            .FirstAsync(x => x.UserId == request.Id, cancellationToken: cancellationToken));
    }
}