using Shared.CQRS;
using EditUserDto = Contracts.UserInfo.EditUserDto;

namespace Features.Users.Commands.EditUser;

public record EditUserCommand(
    string UserId, 
    EditUserDto? EditUserDto) : ICommand;