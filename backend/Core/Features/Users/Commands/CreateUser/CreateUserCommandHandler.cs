using Domain.Entities;
using Domain.Repository;
using Entities.Exceptions;
using Entities.Repository;
using Services;
using Shared.CQRS;
using Shared.Results;

namespace Features.Users.Commands.CreateUser;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IUserBalanceCreatorService _userBalanceCreatorService;
    private readonly IUnitOfWork<IUserRepository> _userRepository;

    public CreateUserCommandHandler(IUserBalanceCreatorService userBalanceCreatorService, IUnitOfWork<IUserRepository> userRepository)
    {
        _userBalanceCreatorService = userBalanceCreatorService;
        _userRepository = userRepository;
    }

    const string ERROR = "Не возможно создать пользователя.";

    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userRepository = _userRepository.Unit;
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
            UserName = $"{DateTime.Now:MMddyyyyHHssmm}",
            UserInfo = new UserInfo { BirthDay = request.Birthdate, UserId = userId }
        };

        try
        {
            await userRepository.CreateUserAsync(user, request.Password, Role.User);
        }
        catch (UserCreationException ex)
        {
            await _userBalanceCreatorService.RollbackAsync();

            return new Error(ex.Message);
        }

        await _userBalanceCreatorService.CommitAsync();
        await _userRepository.SaveChangesAsync();

        return Result.SuccessResult;
    }
}
