using Contracts.Results;
using Shared.CQRS;

namespace Features.Users.Commands.ChangePassword;

public record ChangePasswordCommand(
    string UserId, 
    string OldPassword, 
    string NewPassword) : ICommand<PasswordChangeResult>;