using Results;

namespace Services;

public interface IUserBalanceCreatorService
{
    Task<Result> PrepareBalanceCreationAsync(string userId);

    Task<Result> CommitAsync();

    Task<Result> RollbackAsync();
}
