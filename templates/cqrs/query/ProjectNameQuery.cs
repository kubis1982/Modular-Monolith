namespace %NAMESPACE%
{
    using ModularMonolith.Shared.CQRS.Queries;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public record ProjectNameQuery(int UserId) : Query<ProjectNameQueryResult?>
    {
        internal class ProjectNameQueryHandler : QueryHandler<ProjectNameQuery, ProjectNameQueryResult?>
        {
            public override Task<ProjectNameQueryResult?> Handle(ProjectNameQuery query, CancellationToken cancellationToken)
            {
                return Task.FromResult();
            }
        }
    }


    /// <summary>
    /// Represents the result of the ProjectNameQuery.
    /// </summary>
    public class ProjectNameQueryResult
    {        
    }
}
