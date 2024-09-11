namespace Kubis1982.Shared.CQRS.Queries
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IQueryExecutor
    {
        Task<TResult> ExecuteAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken);
    }
}