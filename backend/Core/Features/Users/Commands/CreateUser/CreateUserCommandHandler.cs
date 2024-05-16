using Carsharing.Contracts;
using Domain.Entities;
using Domain.Repository;
using Entities.Repository;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Services;
using Shared.CQRS;
using Shared.Results;

namespace Features.Users.Commands.CreateUser;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IUserBalanceCreatorService _userBalanceCreatorService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public CreateUserCommandHandler(
        IUserBalanceCreatorService userBalanceCreatorService, 
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IPublishEndpoint publishEndpoint
        )
    {
        _userBalanceCreatorService = userBalanceCreatorService;
        _userRepository = userRepository;
        _publishEndpoint = publishEndpoint;
        _unitOfWork = unitOfWork;
    }

    const string ERROR = "Не возможно создать пользователя.";

    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userRepository = _userRepository;
        var client = await userRepository.GetByEmailAsync(request.Email);
        if (client != null)
            return new Error(ERROR);

        var userId = Guid.NewGuid().ToString();

        var preparationResult = await _userBalanceCreatorService.PrepareBalanceCreationAsync(userId);
        if (!preparationResult.IsSuccess)
            return new Error(ERROR);

        var user = new User
        {
            Id = userId,
            Email = request.Email,
            LastName = request.LastName,
            FirstName = request.Name,
            UserName = $"{DateTime.UtcNow:MMddyyyyHHssmm}",
            UserInfo = new UserInfo { BirthDay = request.Birthdate, UserId = userId }
        };

        try
        {
            await userRepository.CreateUserAsync(user, request.Password, Role.User);
            await _publishEndpoint.Publish(new UserCreatedEvent
            {
                Name = user.FirstName!,
                Roles = [Role.User.ToString()],
                UserId = user.Id
            });

            await _userBalanceCreatorService.CommitAsync();
            await _unitOfWork.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            await _userBalanceCreatorService.RollbackAsync();
        }
        catch (Exception ex)
        {
            await _userRepository.RemoveByIdAsync(userId);
            await _publishEndpoint.Publish(new UserDeletedEvent()
            {
                Id = userId
            });
            await _unitOfWork.SaveChangesAsync();

            return new Error(ex.Message);
        }

        return Result.SuccessResult;
    }
}
