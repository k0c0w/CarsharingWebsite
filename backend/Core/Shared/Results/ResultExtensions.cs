namespace Shared.Results;

public static class ResultExtensions
{
    /// <summary>
    /// Converts Error<typeparamref name="TModel"/> to Error
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="error"></param>
    /// <returns></returns>
    public static Error AsError<TModel>(this Error<TModel> error) => new Error(error.ErrorMessage);

    /// <summary>
    /// Converts Ok<typeparamref name="TModel"/> to Ok
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="error"></param>
    /// <returns></returns>
    public static Ok AsOk<TModel>(this Ok<TModel> ok) => new Ok();

    /// <summary>
    /// Changes operation result data type according to operation result
    /// </summary>
    /// <typeparam name="K">The new type</typeparam>
    /// <param name="data">If result succeed, the data of the new success typel</param>
    /// <param name="errorMessage">If result faulted, error message</param>
    /// <returns></returns>
    public static Result<K> As<TModel, K>(this Result<TModel> result, K? data = default, string? errorMessage = default)
    {
        return result ? new Ok<K>(data!) : new Error<K>(errorMessage);
    }
}
