using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Migrations.CarsharingApp;
using Services.Exceptions;
using Shared.CQRS;
using Shared.Results;

namespace Features.Users.Queries.GetUserWithInfo;

public class GetUserWithInfoQueryHandler : IQueryHandler<GetUserWithInfoQuery, User>
{
    private readonly CarsharingContext _context;

    public GetUserWithInfoQueryHandler(CarsharingContext context)
    {
        _context = context;
    }

    public async Task<Result<User>> Handle(GetUserWithInfoQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(x => x.UserInfo).FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken: cancellationToken);
        if (user == null) throw new ObjectNotFoundException(nameof(User));
        if (user.UserInfo == null)
        {
            throw new InvalidOperationException();
        }

        return new Ok<User>(user);
    }
}