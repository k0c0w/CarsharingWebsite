using Features.Users.Queries.GetUserWithInfo;
using Features.Users.Shared;
using MediatR;
using Migrations.CarsharingApp;
using Services.Exceptions;
using Shared.CQRS;
using Shared.Results;

namespace Features.Users.Commands.EditUser;

public class EditUserCommandHandler : ICommandHandler<EditUserCommand>
{
    private readonly ISender _mediator;
    private readonly UserValidation _userValidation;
    private readonly CarsharingContext _context;

    public EditUserCommandHandler(ISender mediator, UserValidation userValidation, CarsharingContext context)
    {
        _mediator = mediator;
        _userValidation = userValidation;
        _context = context;
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

            var userInfo = user.UserInfo;
            var userEditDto = request.EditUserDto;
            {
                userInfo.PassportType = userEditDto.PassportType;
                userInfo.Passport = userEditDto.Passport;
                userInfo.BirthDay = userEditDto.BirthDay;
                userInfo.DriverLicense = userEditDto.DriverLicense;
                userInfo.Verified = false;
                user.FirstName = userEditDto.FirstName;
                user.LastName = userEditDto.LastName;

                //_context.Users.Update(user);
                _context.UserInfos.Update(userInfo);
            }

            await _context.SaveChangesAsync(cancellationToken);
            return Result.SuccessResult;
        }
        catch (Exception e)
        {
            return new Error(e.Message);
        }
    }
}