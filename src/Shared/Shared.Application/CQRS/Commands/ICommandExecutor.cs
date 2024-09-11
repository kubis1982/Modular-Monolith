namespace Kubis1982.Shared.CQRS.Commands
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface ICommandExecutor
    {
        Task<TResponse> ExecuteAsync<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken);
        Task ExecuteAsync(ICommand command, CancellationToken cancellationToken);
    }
}