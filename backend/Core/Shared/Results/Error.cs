namespace Shared.Results;

public sealed record Error : Result
{
    public Error(string? message = default) : base(false, message)
    {
    }
}

public sealed record Error<TErrorModel> : Result<TErrorModel>
{
    public Error(string? message = default) : base(false, default, message)
    {
    }
}
