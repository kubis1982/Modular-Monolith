namespace Kubis1982.AccessManagement.Commands.Users
{
    using Kubis1982.AccessManagement.Domain.Users;
    using Kubis1982.Shared.CQRS.Commands;
    using System.Threading;
    using System.Threading.Tasks;

    public record ChangePasswordCommand(int UserId, string Password, bool RequirePasswordReset) : UnitOfWorkCommand
    {
        internal class ChangePasswordCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher) : UnitOfWorkCommandHandler<ChangePasswordCommand>
        {
            public override async Task Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
            {
                User user = await userRepository.SingleAsync(UserSpec.ById(command.UserId), cancellationToken);
                UserPassword password = UserPassword.Create(command.Password, passwordHasher);
                user.ChangePassword(password, command.RequirePasswordReset);
            }
        }
    }
}
