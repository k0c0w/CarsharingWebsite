using Entities.Repository;
using FluentValidation;
using MediatR;

namespace Features.Tariffs.Admin.Commands.CreateTariff
{
    public class CreateTariffValidationBehavior : IPipelineBehavior<CreateTariffCommand, Result>
    {
        private readonly IEnumerable<IValidator<CreateTariffCommand>> _validators;
        private readonly ITariffRepository _tariffRepository;

        public CreateTariffValidationBehavior(IEnumerable<IValidator<CreateTariffCommand>> validators, ITariffRepository repository)
        {
            _tariffRepository = repository;
            _validators = validators;
        }

        public async Task<Result> Handle(CreateTariffCommand request, RequestHandlerDelegate<Result> next, CancellationToken cancellationToken)
        {
            foreach(var validator in _validators)
            {
                var validationResult = await validator.ValidateAsync(request);
                if (!validationResult.IsValid)
                    return new Error(validationResult.Errors.First().ErrorMessage);
            }

            // todo: check existing tariffs by name
            // tariff.getByName(comand.name)

            return await next();
        }
    }
}
