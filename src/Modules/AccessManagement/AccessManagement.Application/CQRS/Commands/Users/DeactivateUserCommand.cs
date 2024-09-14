namespace ModularMonolith.Modules.AccessManagement.CQRS.Commands.Users
{
    using ModularMonolith.Modules.AccessManagement.Domain.Users;
    using ModularMonolith.Shared.CQRS.Commands;
    using ModularMonolith.Shared.Security;
    using System.Threading;
    using System.Threading.Tasks;

    public record DeactivateUserCommand(int UserId) : UnitOfWorkCommand
    {
        internal class DeactivateUserCommandHandler(IUserRepository userRepository, IUserContext userContext) : UnitOfWorkCommandHandler<DeactivateUserCommand>
        {
            public override async Task Handle(DeactivateUserCommand command, CancellationToken cancellationToken)
            {
                User user = await userRepository.SingleAsync(UserSpec.ById((UserId)command.UserId), cancellationToken);
                User currentUser = await userRepository.SingleAsync(UserSpec.ById((UserId)userContext.UserId), cancellationToken);
                user.Deactivate(currentUser);
            }
        }
    }
}
