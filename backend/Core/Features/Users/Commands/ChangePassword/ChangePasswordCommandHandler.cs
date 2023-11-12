using Contracts.Results;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Migrations.CarsharingApp;
using Services.Exceptions;
using Shared.CQRS;
using Shared.Results;

namespace Features.Users.Commands.ChangePassword;

public class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand, PasswordChangeResult>
{
    private readonly UserManager<Domain.Entities.User> _userManager;
    private readonly CarsharingContext _context;

    public ChangePasswordCommandHandler(UserManager<User> userManager, CarsharingContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<Result<PasswordChangeResult>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null) 
            return new Error<PasswordChangeResult>(new ObjectNotFoundException(nameof(User)).Message);
        
        var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
        return new Ok<PasswordChangeResult>(new PasswordChangeResult(result.Succeeded, result.Errors.Select(x => x.Description)));

    }
}