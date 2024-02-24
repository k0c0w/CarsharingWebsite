using FluentValidation;

namespace Features.Tariffs.Admin;

public class ChangeTariffStatusCommandValidator : AbstractValidator<ChangeTariffStatusCommand>
{
    public ChangeTariffStatusCommandValidator()
    {
        RuleFor(x => x.Tariffd)
            .GreaterThanOrEqualTo(1)
            .WithMessage($"Wrong tariff id.");
    }
}