namespace Kubis1982.Modules.AccessManagement.Commands.Users
{
    using Kubis1982.Modules.AccessManagement.Domain.Users;
    using Kubis1982.Shared.CQRS.Commands;
    using System.Threading;
    using System.Threading.Tasks;

    public record ChangePasswordCommand(int UserId, string Password) : UnitOfWorkCommand
    {
        internal class ChangePasswordCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher) : UnitOfWorkCommandHandler<ChangePasswordCommand>
        {
            public override async Task Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
            {
                User user = await userRepository.SingleAsync(UserSpec.ById(command.UserId), cancellationToken);
                UserPassword password = UserPassword.Create(command.Password, passwordHasher);
                user.ChangePassword(password);
            }
        }
    }
}
