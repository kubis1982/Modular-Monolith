namespace ModularMonolith.Shared.CQRS.Commands
{
    public interface IUnitOfWorkCommand : ICommandBase { }

    public abstract record UnitOfWorkCommand : Command, IUnitOfWorkCommand
    {
    }

    public abstract class UnitOfWorkCommandHandler<TRequest> : CommandHandler<TRequest> where TRequest : UnitOfWorkCommand
    {
    }

    public abstract record UnitOfWorkCommand<TResponse> : Command<TResponse>, IUnitOfWorkCommand
    {
    }

    public abstract class UnitOfWorkCommandHandler<TRequest, TResponse> : CommandHandler<TRequest, TResponse> where TRequest : UnitOfWorkCommand<TResponse>
    {
    }
}
