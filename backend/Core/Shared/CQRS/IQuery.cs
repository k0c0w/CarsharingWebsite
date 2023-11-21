using MediatR;
using Shared.Results;

namespace Shared.CQRS
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>> { }
}
