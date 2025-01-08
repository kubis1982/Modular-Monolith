namespace ModularMonolith.Modules.AccessManagement.CQRS.Commands.Users
{
    using FluentValidation;
    using ModularMonolith.Modules.AccessManagement.Domain.Users;
    using ModularMonolith.Shared.CQRS.Commands;
    using ModularMonolith.Shared.Time;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public record ChangePasswordByTokenCommand(Guid Token, string Password) : UnitOfWorkCommand
    {
        internal class ChangePasswordByTokenCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IClock clock) : UnitOfWorkCommandHandler<ChangePasswordByTokenCommand>
        {
            public override async Task Handle(ChangePasswordByTokenCommand command, CancellationToken cancellationToken)
            {
                User user = await userRepository.SingleAsync(UserSpec.ByToken(command.Token), cancellationToken);
                UserPassword userPassword = UserPassword.Create(command.Password, passwordHasher);
                user.FinishToken(command.Token, userPassword, clock);
            }
        }

        internal class ChangePasswordByTokenCommandValidator : AbstractValidator<ChangePasswordByTokenCommand>
        {
            public ChangePasswordByTokenCommandValidator()
            {
                RuleFor(n => n.Token)
                    .NotEmpty().NotEqual(Guid.Empty);
                RuleFor(n => n.Password)
                    .NotEmpty();
            }
        }
    }
}
