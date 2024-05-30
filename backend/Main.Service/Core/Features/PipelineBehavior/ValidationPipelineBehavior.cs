using FluentValidation;
using MediatR;

namespace Features.PipelineBehavior;

public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }   
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
                return await next();
        }

        var errors = _validators
            .Select(async validator => await validator.ValidateAsync(request, cancellationToken))
            .SelectMany(x => x.Result.Errors)
            .Where(result => result is not null)
            .Select(error => new Error(error.ErrorMessage))
            .Distinct();

        /*if (errors.Any())
            return ReturnResult<TResponse>(errors.First().ErrorMessage);
        */
        return await next();
    }

    private static TResult ReturnResult<TResult>(string message)
        where TResult : Result
    {
        if (typeof(TResult) == typeof(Result))
            return (dynamic)new Error(message);
        
        dynamic result;
        
        result = typeof(Error)
            .GetGenericTypeDefinition()
            .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
            .GetConstructor(new Type[] { typeof(string) })!
            .Invoke(null, new object[] { message })!;
        
        if (result is null)
            throw new Exception();

        return (TResult)result;
    }
}