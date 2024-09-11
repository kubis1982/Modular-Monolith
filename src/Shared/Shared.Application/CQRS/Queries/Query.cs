using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Kubis1982.Shared.CQRS.Queries
{

    public abstract record Query<TResponse> : IQuery<TResponse>
    {
    }

    public abstract class QueryHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IQuery<TResponse>
    {
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
