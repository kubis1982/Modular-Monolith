namespace ModularMonolith.Modules.AccessManagement.CQRS.Commands.Users
{
    using ModularMonolith.Modules.AccessManagement.Domain.Users;
    using ModularMonolith.Shared.CQRS.Commands;
    using ModularMonolith.Shared.Time;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record RefreshSessionCommand(int SessionId, DateTime ExpiryDate, string RefreshToken, string NewRefreshToken, DateTime ExpiryRefreshToken) : UnitOfWorkCommand
    {
        internal class RefreshSessionCommandHandler(IUserRepository userRepository, IClock clock) : UnitOfWorkCommandHandler<RefreshSessionCommand>
        {
            public override async Task Handle(RefreshSessionCommand command, CancellationToken cancellationToken)
            {
                User user = await userRepository.SingleAsync(UserSpec.BySessionId((SessionId)command.SessionId), cancellationToken);
                RefreshToken newRefreshToken = Domain.Users.RefreshToken.Create(command.NewRefreshToken, command.ExpiryRefreshToken);
                user.RefreshSession((SessionId)command.SessionId, command.RefreshToken, command.ExpiryDate, newRefreshToken, clock);
            }
        }
    }
}
