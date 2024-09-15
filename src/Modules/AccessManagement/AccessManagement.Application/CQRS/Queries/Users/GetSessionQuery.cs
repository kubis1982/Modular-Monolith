namespace ModularMonolith.Modules.AccessManagement.CQRS.Queries.Users
{
    using ModularMonolith.Shared.CQRS.Queries;
    using ModularMonolith.Shared.Time;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record GetSessionQuery(int SessionId) : Query<GetSessionQueryResult?>
    {
        internal class GetSessionsQueryHandler(IQueryContext queryContext, IClock clock) : QueryHandler<GetSessionQuery, GetSessionQueryResult?>
        {
            public override Task<GetSessionQueryResult?> Handle(GetSessionQuery request, CancellationToken cancellationToken)
            {
                DateTime dt = clock.Now;
                return Task.FromResult((from sessions in queryContext.Sessions.Where(n => n.Id == request.SessionId)
                            orderby sessions.CreatedOn descending
                            select new GetSessionQueryResult
                            {
                                Id = sessions.Id,
                                CreatedBy = sessions.CreatedBy,
                                CreatedOn = sessions.CreatedOn,
                                IsExpired = dt > sessions.ExpiryDate,
                                ExpiryDate = sessions.ExpiryDate,
                                RefreshToken = sessions.RefreshToken,
                                RefreshTokenExpiryDate = sessions.RefreshTokenExpiryDate
                            }).SingleOrDefault());
            }
        }
    }

    public class GetSessionQueryResult
    {
        public int Id { get; set; }
        public bool IsExpired { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryDate { get; set; }
    }
}
