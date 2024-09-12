namespace Kubis1982.Modules.AccessManagement.CQRS.Commands.Users
{
    using Kubis1982.Modules.AccessManagement.Domain.Users;
    using Kubis1982.Shared.CQRS.Commands;
    using System.Threading;
    using System.Threading.Tasks;

    public record UpdateUserCommand(int UserId, string? FirstName, string? MiddleName, string? LastName) : UnitOfWorkCommand
    {
        internal class UpdateUserCommandHandler(IUserRepository userRepository) : UnitOfWorkCommandHandler<UpdateUserCommand>
        {
            public override async Task Handle(UpdateUserCommand command, CancellationToken cancellationToken)
            {
                User user = await userRepository.SingleAsync(UserSpec.ById(command.UserId), cancellationToken);
                UserFullName fullName = UserFullName.Create(command.FirstName, command.MiddleName, command.LastName);
                user.Update(fullName);
            }
        }
    }
}
