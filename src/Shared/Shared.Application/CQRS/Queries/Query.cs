using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ModularMonolith.Shared.CQRS.Queries
{

    public abstract record Query<TResponse> : IQuery<TResponse>
    {
    }

    public abstract class QueryHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IQuery<TResponse>
    {
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
