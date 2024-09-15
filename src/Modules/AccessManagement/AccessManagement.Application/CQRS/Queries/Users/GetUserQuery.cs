namespace ModularMonolith.Modules.AccessManagement.CQRS.Queries.Users
{
    using ModularMonolith.Shared.CQRS.Queries;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a query to get a user by their ID.
    /// </summary>
    public record GetUserQuery(int UserId) : Query<GetUserQueryResult?>
    {
        internal class GetUserQueryHandler(IQueryContext readQueryable) : QueryHandler<GetUserQuery, GetUserQueryResult?>
        {
            /// <summary>
            /// Handles the GetUserQuery and returns the GetUserQueryResult.
            /// </summary>
            /// <param name="query">The GetUserQuery.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>The GetUserQueryResult.</returns>
            public override Task<GetUserQueryResult?> Handle(GetUserQuery query, CancellationToken cancellationToken)
            {
                return Task.FromResult((from user in readQueryable.Users.Where(n => n.Id == query.UserId)
                                        select new GetUserQueryResult
                                        {
                                            TypeId = user.TypeId,
                                            Id = user.Id,
                                            Email = user.Email,
                                            FirstName = user.FirstName,
                                            LastName = user.LastName,
                                            MiddleName = user.MiddleName,
                                            IsActive = user.IsActive,
                                            LastLogged = readQueryable.Sessions.Where(m => m.CreatedBy == user.Id).OrderByDescending(n => n.CreatedOn).Select(n => n.CreatedOn).FirstOrDefault()
                                        }).SingleOrDefault());
            }
        }
    }


    /// <summary>
    /// Represents the result of the GetUserQuery.
    /// </summary>
    public class GetUserQueryResult
    {
        /// <summary>
        /// Gets or sets the TypeId of the user.
        /// </summary>
        public required string TypeId { get; set; }

        /// <summary>
        /// Gets or sets the Id of the user.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Email of the user.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the FirstName of the user.
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// Gets or sets the MiddleName of the user.
        /// </summary>
        public string? MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the LastName of the user.
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user is active.
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// Gets or sets the last logged date of the user.
        /// </summary>
        public DateTime? LastLogged { get; set; }
    }
}
