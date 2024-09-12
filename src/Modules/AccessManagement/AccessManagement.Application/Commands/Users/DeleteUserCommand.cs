namespace Kubis1982.Modules.AccessManagement.Commands.Users
{
    using Kubis1982.Modules.AccessManagement.Domain.Users;
    using Kubis1982.Shared.CQRS.Commands;
    using Kubis1982.Shared.Security;
    using System.Threading;
    using System.Threading.Tasks;

    public record DeleteUserCommand(int UserId) : UnitOfWorkCommand
    {
        internal class DeleteUserCommandHandler(IUserRepository userRepository, IUserContext userContext) : UnitOfWorkCommandHandler<DeleteUserCommand>
        {
            public override async Task Handle(DeleteUserCommand command, CancellationToken cancellationToken)
            {
                var user = await userRepository.SingleAsync(UserSpec.ById(command.UserId), cancellationToken);
                var currentUser = await userRepository.SingleAsync(UserSpec.ById(userContext.UserId), cancellationToken);
                user.Delete(currentUser);
                await userRepository.DeleteAsync(user, cancellationToken);
            }
        }
    }
}
