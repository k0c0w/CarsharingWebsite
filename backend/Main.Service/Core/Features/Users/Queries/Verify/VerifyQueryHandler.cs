using Features.Users.Queries.GetUserWithInfo;
using MediatR;
using Migrations.CarsharingApp;

namespace Features.Users.Queries.Verify;

public class VerifyQueryHandler : IQueryHandler<VerifyQuery, bool>
{
    private readonly CarsharingContext _context;
    private readonly IMediator _mediator;

    public VerifyQueryHandler(CarsharingContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<Result<bool>> Handle(VerifyQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = (await _mediator.Send(new GetUserWithInfoQuery(request.UserId), cancellationToken));
            if (result.IsSuccess is not true || result.Value is null)
                return new Error<bool>();

            var user = result.Value.UserInfo;
            user.Verified = true;
            user.User.EmailConfirmed = true;
            _context.UserInfos.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
            return new Ok<bool>(true);
        }
        catch (Exception e)
        {
            return new Error<bool>(e.Message);
        }
    }
}