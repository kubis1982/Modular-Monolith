using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Kubis1982.Shared.CQRS.Commands
{

    public abstract record Command<TResponse> : ICommand<TResponse>
    {
    }

    public abstract class CommandHandler<TRequest, TResponse> : ICommandHandler<TRequest, TResponse> where TRequest : ICommand<TResponse>
    {
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }

    public abstract record Command : ICommand { }

    public abstract class CommandHandler<TRequest> : ICommandHandler<TRequest> where TRequest : ICommand
    {
        public abstract Task Handle(TRequest request, CancellationToken cancellationToken);
    }
}
