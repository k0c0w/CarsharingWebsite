using FluentValidation;

namespace Features.Tariffs.Admin;

public class DeleteTariffCommandValidator : AbstractValidator<DeleteTariffCommand>
{
    public DeleteTariffCommandValidator()
    {
        RuleFor(x => x.TariffId)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Wrong tariff id.");
    }
}