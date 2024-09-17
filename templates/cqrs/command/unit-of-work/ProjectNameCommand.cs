namespace %NAMESPACE%
{
    using ModularMonolith.Shared.CQRS.Commands;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public record ProjectNameCommand : UnitOfWorkCommand
    {
        internal class ProjectNameCommandHandler : UnitOfWorkCommandHandler<ProjectNameCommand>
        {
            public override Task Handle(ProjectNameCommand command, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
