namespace ModularMonolith.Modules.AccessManagement.CQRS.Commands.Users
{
    using ModularMonolith.Modules.AccessManagement.Domain.Users;
    using ModularMonolith.Shared.CQRS.Commands;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public record CreateSessionCommand(string Email, string Password, DateTime ExpiryDate, string RefreshToken, DateTime ExpiryRefreshToken) : EntityCommand
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="passwordHasher"></param>
        /// <param name="userRepository"></param>
        internal class CreateSessionCommandHandler(IPasswordHasher passwordHasher, IUserRepository userRepository) : EntityCommandHandler<CreateSessionCommand>
        {
            public override async Task<EntityIdentityResult> Handle(CreateSessionCommand command, CancellationToken cancellationToken)
            {
                User user = await userRepository.SingleAsync(UserSpec.ByEmail((UserEmail)command.Email), cancellationToken);
                RefreshToken refreshToken = Domain.Users.RefreshToken.Create(command.RefreshToken, command.ExpiryRefreshToken);
                UserPassword password = UserPassword.Create(command.Password, passwordHasher);
                Session session = user.CreateSession(password, command.ExpiryDate, refreshToken);
                return EntityIdentityResult.Create(session);
            }
        }
    }
}
