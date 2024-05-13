using Carsharing.Contracts;
using Domain;
using MassTransit;
using Services.Abstractions;

namespace Chat.Persistance;

public class UserCreatedEventConsumer(IUserService userService) : IConsumer<UserCreatedEvent>
{
    private readonly IUserService _userService = userService;

    public Task Consume(ConsumeContext<UserCreatedEvent> context)
    {
        var message = context.Message;

        var user = new User
        {
            Id = message.UserId,
            Name = message.Name,
            IsManager = message.Roles.Contains("Manager"),
        };

        return _userService.TryCreateUserAsync(user);
    }
}
