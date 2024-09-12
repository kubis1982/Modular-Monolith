namespace Kubis1982.Shared.CQRS.Commands
{
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    internal class CommandExecutor(IMediator mediator) : ICommandExecutor
    {
        public Task<TResponse> ExecuteAsync<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken) => mediator.Send(command, cancellationToken);

        public Task ExecuteAsync(ICommand command, CancellationToken cancellationToken) => mediator.Send(command, cancellationToken);
    }
}
