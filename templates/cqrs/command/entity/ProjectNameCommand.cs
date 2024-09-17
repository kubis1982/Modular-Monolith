namespace %NAMESPACE%
{
    using ModularMonolith.Shared.CQRS.Commands;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public record ProjectNameCommand : EntityCommand
    {
        internal class ProjectNameCommandHandler : EntityCommandHandler<ProjectNameCommand>
        {
            public override Task<EntityIdentityResult> Handle(ProjectNameCommand command, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
