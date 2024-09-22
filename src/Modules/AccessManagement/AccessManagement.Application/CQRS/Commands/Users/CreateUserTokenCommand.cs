using FluentValidation;
using ModularMonolith.Modules.AccessManagement.Domain.Users;
using ModularMonolith.Shared.CQRS.Commands;
using ModularMonolith.Shared.Time;
using System.Threading;
using System.Threading.Tasks;

namespace ModularMonolith.Modules.AccessManagement.CQRS.Commands.Users
{
    public record CreateUserTokenCommand(string Email) : UnitOfWorkCommand
    {
        internal class CreateUserTokenCommandHandler(IUserRepository userRepository, IClock clock) : UnitOfWorkCommandHandler<CreateUserTokenCommand>
        {
            public override async Task Handle(CreateUserTokenCommand command, CancellationToken cancellationToken)
            {
                User user = await userRepository.SingleAsync(UserSpec.ByEmail((UserEmail)command.Email), cancellationToken);
                user.CreateToken(clock);
            }
        }

        internal class CreateUserTokenCommandValidator : AbstractValidator<CreateUserTokenCommand>
        {
            public CreateUserTokenCommandValidator()
            {
                RuleFor(n => n.Email).NotEmpty();
            }

        }
    }
}
