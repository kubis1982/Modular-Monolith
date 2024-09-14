namespace ModularMonolith.Modules.AccessManagement.CQRS.Commands.Users
{
    using ModularMonolith.Modules.AccessManagement.Domain.Users;
    using ModularMonolith.Shared.CQRS.Commands;
    using System.Threading;
    using System.Threading.Tasks;

    public record ChangePasswordCommand(int UserId, string Password) : UnitOfWorkCommand
    {
        internal class ChangePasswordCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher) : UnitOfWorkCommandHandler<ChangePasswordCommand>
        {
            public override async Task Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
            {
                User user = await userRepository.SingleAsync(UserSpec.ById((UserId)command.UserId), cancellationToken);
                UserPassword password = UserPassword.Create(command.Password, passwordHasher);
                user.ChangePassword(password);
            }
        }
    }
}
