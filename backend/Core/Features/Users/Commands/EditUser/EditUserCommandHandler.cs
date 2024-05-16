using Carsharing.Contracts.UserEvents;
using Contracts.UserInfo;
using Domain.Entities;
using Domain.Repository;
using Entities.Repository;
using Features.Users.Queries.GetUserWithInfo;
using Features.Users.Shared;
using MassTransit;
using MassTransit.Initializers;
using MediatR;
using Services.Exceptions;
using Shared.CQRS;
using Shared.Results;

namespace Features.Users.Commands.EditUser;

public class EditUserCommandHandler : ICommandHandler<EditUserCommand>
{
    private readonly ISender _mediator;
    private readonly UserValidation _userValidation;
    private readonly IUserRepository _userRepository;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IUnitOfWork _unitOfWork;

    public EditUserCommandHandler(ISender mediator, UserValidation userValidation, IUserRepository userRepository, IPublishEndpoint publishEndpoint, IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _userValidation = userValidation;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Result> Handle(EditUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new GetUserWithInfoQuery(request.UserId), cancellationToken);
            if (!result.IsSuccess)
                return new Error();
            var user = result.Value!;
            if(! await _userValidation.CheckUserEmail(user, request.EditUserDto!.Email!)) {throw new AlreadyExistsException();}
            UserValidation.CheckLastName(user,request.EditUserDto.LastName!);
            UserValidation.CheckName(user, request.EditUserDto.FirstName!);
            UserValidation.CheckUserBirthday(user.UserInfo, request.EditUserDto.BirthDay);
            UserValidation.CheckUserPassport(user.UserInfo, request.EditUserDto!.Passport!);
            UserValidation.CheckUserPassportType(user.UserInfo, request.EditUserDto!.PassportType!);

            var userEditDto = request.EditUserDto;
            UpdateUserInfoFields(user.UserInfo, userEditDto);
            await UpdateUserFieldsAsync(user, userEditDto);

            await _userRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return Result.SuccessResult;
        }
        catch (Exception e)
        {
            return new Error(e.Message);
        }
    }

    private void UpdateUserInfoFields(UserInfo userInfo, EditUserDto userEditDto)
    {
        userInfo.PassportType = userEditDto.PassportType;
        userInfo.Passport = userEditDto.Passport;
        userInfo.BirthDay = userEditDto.BirthDay;
        userInfo.DriverLicense = userEditDto.DriverLicense;
        userInfo.Verified = false;
    }

    private async Task UpdateUserFieldsAsync(User user, EditUserDto userEditDto)
    {
        user.FirstName = userEditDto.FirstName;
        user.LastName = userEditDto.LastName;

        var roles = await _userRepository.GetUserRolesAsync(user.Id);
        await _publishEndpoint.Publish(new UserUpdatedEvent
        {
            UserId = user.Id,
            Name = user.FirstName,
            Roles = roles.Select(x => x.ToString()).ToArray(),
        });
    }
}