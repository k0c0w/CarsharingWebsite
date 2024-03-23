using Shared.Results;

namespace Services;

public interface IBookCarService
{
    // here we should assign cars


    Task<Result> PrepareCarAssigmentAsync(int carId);

    Task<Result> CommitCarAssigmentAsync();

    Task<Result> RollbackCarAssigmentAsync();
}
