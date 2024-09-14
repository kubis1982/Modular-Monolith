namespace ModularMonolith.Shared.CQRS.Queries
{
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    internal class QueryExecutor(IMediator mediator) : IQueryExecutor
    {
        public Task<TResult> Get<TResult>(IQuery<TResult> query, CancellationToken cancellationToken) => mediator.Send(query, cancellationToken);
    }
}
