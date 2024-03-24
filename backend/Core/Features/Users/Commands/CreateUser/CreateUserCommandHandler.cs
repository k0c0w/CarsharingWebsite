using Domain.Entities;
using Domain.Repository;
using Entities.Repository;
using Microsoft.AspNetCore.Identity;
using Services;
using Shared.CQRS;
using Shared.Results;
using System.Security.Claims;


namespace Features.Users.Commands.CreateUser;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    private readonly IUserBalanceCreatorService _userBalanceCreatorService;
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork<IUserRepository> _userRepository;

    public CreateUserCommandHandler(IUserBalanceCreatorService userBalanceCreatorService, UserManager<User> userManager, IUnitOfWork<IUserRepository> userRepository)
    {
        _userManager = userManager;
        _userBalanceCreatorService = userBalanceCreatorService;
        _userRepository = userRepository;
    }

    const string ERROR = "Не возможно создать пользователя.";

    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var client = await _userManager.FindByEmailAsync(request.Email);
        if (client != null)
            return new Error(ERROR);

        var userId = Guid.NewGuid().ToString();

        var preparationResult = await _userBalanceCreatorService.PrepareBalanceCreationAsync(userId);
        if (!preparationResult.IsSuccess)
            return new Error(ERROR);

        var user = new User { Id = userId, Email = request.Email, LastName = request.LastName, FirstName = request.Name, UserName = $"{DateTime.Now:MMddyyyyHHssmm}" };
        var resultUserCreate = await _userManager.CreateAsync(user, request.Password);

        if (!resultUserCreate.Succeeded)
        {
            await _userBalanceCreatorService.RollbackAsync();

            return new Error(resultUserCreate.Errors.Select(x => x.Description).FirstOrDefault());
        }

        user.UserInfo = new UserInfo { BirthDay = request.Birthdate, UserId = userId };
        await Task.WhenAll(
            _userRepository.Unit.UpdateAsync(user),
            _userManager.AddToRoleAsync(user, Role.User.ToString()),
            _userBalanceCreatorService.CommitAsync()
            );

        await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.NameIdentifier, userId));
        await _userRepository.SaveChangesAsync();

        return Result.SuccessResult;
    }
}
