namespace Kubis1982.Shared.CQRS.Commands
{
    using MediatR;

    public interface ICommandBase : IBaseRequest
    {
    }

    public interface ICommand : ICommandBase, IRequest
    {
    }

    public interface ICommandHandler<TRequest> : IRequestHandler<TRequest> where TRequest : ICommand
    {
    }

    public interface ICommand<TResponse> : ICommandBase, IRequest<TResponse>
    {
    }

    public interface ICommandHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : ICommand<TResponse>
    {
    }
}
