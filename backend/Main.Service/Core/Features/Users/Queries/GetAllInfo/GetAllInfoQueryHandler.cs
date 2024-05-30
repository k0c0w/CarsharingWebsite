using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Migrations.CarsharingApp;

namespace Features.Users.Queries.GetAllInfo;

public class GetAllInfoQueryHandler : IQueryHandler<GetAllInfoQuery, List<UserInfo>>
{
    private readonly CarsharingContext _context;

    public GetAllInfoQueryHandler(CarsharingContext context)
    {
        _context = context;
    }

    public async Task<Result<List<UserInfo>>> Handle(GetAllInfoQuery request, CancellationToken cancellationToken)
    {
        var userInfos = await _context.UserInfos
            .AsNoTracking()
            .Include(u => u.User)
            .ToListAsync(cancellationToken: cancellationToken);
        return new Ok<List<UserInfo>>(userInfos);
    }
}