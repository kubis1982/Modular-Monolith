namespace ModularMonolith.Modules.AccessManagement.CQRS.Commands.Users
{
    using ModularMonolith.Modules.AccessManagement.Domain.Users;
    using ModularMonolith.Shared.CQRS.Commands;
    using ModularMonolith.Shared.Security;
    using System.Threading;
    using System.Threading.Tasks;

    public record DeleteUserCommand(int UserId) : UnitOfWorkCommand
    {
        internal class DeleteUserCommandHandler(IUserRepository userRepository, IUserContext userContext) : UnitOfWorkCommandHandler<DeleteUserCommand>
        {
            public override async Task Handle(DeleteUserCommand command, CancellationToken cancellationToken)
            {                
                var user = await userRepository.SingleAsync(UserSpec.ByIdWithLastSession((UserId)command.UserId), cancellationToken);
                var currentUser = await userRepository.SingleAsync(UserSpec.ById((UserId)userContext.Id), cancellationToken);
                user.Delete(currentUser);
                await userRepository.DeleteAsync(user, cancellationToken);
            }
        }
    }
}
