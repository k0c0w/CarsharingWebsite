namespace Shared.Results;

public abstract record Result
{
    private static readonly Ok _defaultSuccessResult = new Ok();
    private static readonly Error _defaultErrorResult = new Error();

    public static Ok SuccessResult => _defaultSuccessResult;

    public static Error ErrorResult => _defaultErrorResult;

    public bool IsSuccess { get; }

    public string? ErrorMessage { get; }

    protected Result(bool isSuccessful, string? error = default)
    {
        if (isSuccessful && !string.IsNullOrEmpty(error))
            throw new ArgumentException($"Unexpected value: \"{error}\"", nameof(error));

        IsSuccess = isSuccessful;
        ErrorMessage = error;
    }

    public static bool operator true(Result result) => result.IsSuccess;

    public static bool operator false(Result result) => !result.IsSuccess;

    public static bool operator !(Result result) => !result.IsSuccess;
}

public abstract record Result<TModel> : Result
{
    public TModel? Value { get; }

    protected Result(bool isSuccessful, TModel? model = default, string? error = default) : base(isSuccessful, error)
    {
        if (isSuccessful && model == null)
            throw new ArgumentNullException($"Successful {GetType()} must specify {nameof(Value)}.");

        Value = model;
    }
}
