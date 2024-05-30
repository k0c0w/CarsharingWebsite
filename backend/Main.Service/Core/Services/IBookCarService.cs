using Results;

namespace Services;

public interface IBookCarService
{
    Task<Result> PrepareCarAssignmentAsync(int carId);

    Task<Result> CommitCarAssignmentAsync();

    Task<Result> RollbackCarAssignmentAsync();
}
