namespace Kubis1982.AccessManagement.Commands.Users
{
    using Kubis1982.AccessManagement.Domain.Users;
    using Kubis1982.Shared.CQRS.Commands;
    using Kubis1982.Shared.Security;
    using System.Threading;
    using System.Threading.Tasks;

    public record ActivateUserCommand(int UserId) : UnitOfWorkCommand
    {
        internal class ActivateUserCommandHandler(IUserRepository userRepository, IUserContext userContext) : UnitOfWorkCommandHandler<ActivateUserCommand>
        {
            public override async Task Handle(ActivateUserCommand command, CancellationToken cancellationToken)
            {
                User user = await userRepository.SingleAsync(UserSpec.ById(command.UserId), cancellationToken);
                User currentUser = await userRepository.SingleAsync(UserSpec.ById(userContext.UserId), cancellationToken);
                user.Activate(currentUser);
            }
        }
    }
}
