namespace ModularMonolith.Shared.CQRS.Commands
{
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    internal class CommandExecutor(IMediator mediator) : ICommandExecutor
    {
        public Task<TResponse> Execute<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken) => mediator.Send(command, cancellationToken);

        public Task Execute(ICommand command, CancellationToken cancellationToken) => mediator.Send(command, cancellationToken);
    }
}
