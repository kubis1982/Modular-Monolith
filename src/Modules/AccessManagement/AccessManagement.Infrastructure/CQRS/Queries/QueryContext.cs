using ModularMonolith.Modules.AccessManagement.Persistance.ReadModel;

namespace ModularMonolith.Modules.AccessManagement.CQRS.Queries
{
    using System.Linq;

    internal class QueryContext(ReadDbContext readDbContext) : IQueryContext
    {
        public IQueryable<UserEntity> Users => readDbContext.Users;

        public IQueryable<SessionEntity> Sessions => readDbContext.Sessions;
    }
}
