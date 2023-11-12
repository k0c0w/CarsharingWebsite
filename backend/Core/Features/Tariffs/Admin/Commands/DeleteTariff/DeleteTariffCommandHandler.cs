using Entities.Repository;
using Shared.CQRS;
using Shared.Results;

namespace Features.Tariffs.Admin;

public class DeleteTariffCommandHandler : ICommandHandler<DeleteTariffCommand>
{
    private readonly ITariffRepository _tariffs;

    public DeleteTariffCommandHandler(ITariffRepository repository)
    {
        _tariffs = repository;
    }

    public async Task<Result> Handle(DeleteTariffCommand command, CancellationToken cancellationToken)
    {
        if (command.TariffId < 1)
            return new Error("Wrong tariff id.");

        try
        {
            await _tariffs.RemoveByIdAsync(command.TariffId);

            return Result.SuccessResult;
        }
        catch (Exception ex)
        {
            return new Error(ex.Message);
        }
    }
}
