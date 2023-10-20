using Shared.Results;
using MediatR;

namespace Shared.CQRS;

public interface ICommand : IRequest<Result> { }

public interface ICommand<TResponse> : IRequest<Result<TResponse>> { }