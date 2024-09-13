namespace Kubis1982.Shared.CQRS.Commands
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface ICommandExecutor
    {
        Task<TResponse> Execute<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken);
        Task Execute(ICommand command, CancellationToken cancellationToken);
    }
}
