﻿using Shared.Results;

namespace Services;

public interface IBookCarService
{
    Task<Result> PrepareCarAssigmentAsync(int carId);

    Task<Result> CommitCarAssigmentAsync();

    Task<Result> RollbackCarAssigmentAsync();
}
