namespace Shared.Results;

public sealed record Ok : Result
{
    public Ok() : base(true)
    {
    }
}

public sealed record Ok<TModel> : Result<TModel>
{
    public Ok(TModel model) : base(true, model)
    {
    }
}
