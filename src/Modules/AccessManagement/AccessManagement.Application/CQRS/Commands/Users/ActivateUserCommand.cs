namespace ModularMonolith.Modules.AccessManagement.CQRS.Commands.Users
{
    using ModularMonolith.Modules.AccessManagement.Domain.Users;
    using ModularMonolith.Shared.CQRS.Commands;
    using ModularMonolith.Shared.Security;
    using System.Threading;
    using System.Threading.Tasks;

    public record ActivateUserCommand(int UserId) : UnitOfWorkCommand
    {
        internal class ActivateUserCommandHandler(IUserRepository userRepository, IUserContext userContext) : UnitOfWorkCommandHandler<ActivateUserCommand>
        {
            public override async Task Handle(ActivateUserCommand command, CancellationToken cancellationToken)
            {
                User user = await userRepository.SingleAsync(UserSpec.ById((UserId)command.UserId), cancellationToken);
                User currentUser = await userRepository.SingleAsync(UserSpec.ById((UserId)userContext.Id), cancellationToken);
                user.Activate(currentUser);
            }
        }
    }
}
