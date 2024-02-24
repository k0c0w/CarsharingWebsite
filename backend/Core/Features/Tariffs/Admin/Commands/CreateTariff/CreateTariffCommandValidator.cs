using FluentValidation;
using Shared.Results;

namespace Features.Tariffs.Admin;

public class CreateTariffCommandValidator : AbstractValidator<CreateTariffCommand>
{
    public CreateTariffCommandValidator()
    {
        RuleFor(x => x)
            .Must(x => AreValidParameters(x).IsSuccess)
            .WithMessage(x=>AreValidParameters(x).ErrorMessage);
    } 
    
    private Result AreValidParameters(CreateTariffCommand command)
    {
        if (string.IsNullOrEmpty(command.Name))
            return new Error("Tariff name was empty.");

        if (command.Name.Length > 512)
            return new Error("Tariff name was too long.");

        return Result.SuccessResult; 
    }
}