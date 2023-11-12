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
        await _tariffs.RemoveByIdAsync(command.TariffId).ConfigureAwait(false);
        return Result.SuccessResult;
    }
}
