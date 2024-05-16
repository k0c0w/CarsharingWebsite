using Carsharing.Contracts.UserEvents;
using MassTransit;
using Services.Abstractions;

namespace Chat.Persistance.Consumers;

public class UserEventsConsumer(IUserService userService) : IConsumer<UserCreatedEvent>, IConsumer<UserDeletedEvent>, IConsumer<UserUpdatedEvent>
{
    private readonly IUserService _userService = userService;

    public Task Consume(ConsumeContext<UserCreatedEvent> context)
    {
        var message = context.Message;

        var userInfo = new UserInfoDto(message.UserId, message.Name, message.Roles);

        return _userService.TryCreateUserAsync(userInfo);
    }

    public Task Consume(ConsumeContext<UserDeletedEvent> context)
    {
        return _userService.TryDeleteUserAsync(context.Message.UserId);
    }

    public Task Consume(ConsumeContext<UserUpdatedEvent> context)
    {
        var message = context.Message;

        var userInfo = new UserInfoDto(message.UserId, message.Name, message.Roles);

        return _userService.TryUpdateUserAsync(userInfo);
    }
}
