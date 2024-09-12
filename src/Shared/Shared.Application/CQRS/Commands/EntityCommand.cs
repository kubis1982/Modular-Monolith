namespace Kubis1982.Shared.CQRS.Commands
{
    public abstract record EntityCommand : UnitOfWorkCommand<EntityIdentityResult>
    {
    }

    public abstract class EntityCommandHandler<TRequest> : UnitOfWorkCommandHandler<TRequest, EntityIdentityResult> where TRequest : UnitOfWorkCommand<EntityIdentityResult>
    {
    }
}
