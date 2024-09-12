namespace Kubis1982.Modules.AccessManagement.Commands.Users
{
    using Kubis1982.AccessManagement.Domain.Users;
    using Kubis1982.Modules.AccessManagement.Domain.Users;
    using Kubis1982.Shared.CQRS.Commands;
    using System.Threading;
    using System.Threading.Tasks;

    public record UpdateUserCommand(int UserId, string Name, string? FirstName, string? MiddleName, string? LastName, string? Email) : UnitOfWorkCommand
    {
        internal class UpdateUserCommandHandler(IUserRepository userRepository) : UnitOfWorkCommandHandler<UpdateUserCommand>
        {
            public override async Task Handle(UpdateUserCommand command, CancellationToken cancellationToken)
            {
                User user = await userRepository.SingleAsync(UserSpec.ById(command.UserId), cancellationToken);
                UserName userName = UserName.Of(command.Name);
                UserFullName fullName = UserFullName.Create(command.FirstName, command.MiddleName, command.LastName);
                UserEmail? userEmail = UserEmail.Of(command.Email);
                user.Update(userName, fullName, userEmail);
            }
        }
    }
}
