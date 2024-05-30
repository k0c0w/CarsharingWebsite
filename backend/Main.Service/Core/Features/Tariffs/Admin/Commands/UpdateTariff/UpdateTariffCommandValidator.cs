using FluentValidation;

namespace Features.Tariffs.Admin;

public class UpdateTariffCommandValidator : AbstractValidator<UpdateTariffCommand>
{
    public UpdateTariffCommandValidator()
    {
        RuleFor(x => x)
            .Must(command => ValidateParams(command).IsSuccess)
            .WithMessage(command => ValidateParams(command).ErrorMessage);
    }
    
    private Result ValidateParams(UpdateTariffCommand command)
    {
        //todo: validate params
        if (command.TariffId < 1)
            return new Error("Incorrect id");

        if (String.IsNullOrWhiteSpace(command.Name))
            return new Error("Incorrect name");

        return Result.SuccessResult;
    }
}