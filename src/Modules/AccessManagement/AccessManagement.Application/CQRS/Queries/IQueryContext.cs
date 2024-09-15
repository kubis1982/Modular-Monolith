namespace ModularMonolith.Modules.AccessManagement.CQRS.Queries
{
    using ModularMonolith.Modules.AccessManagement.Persistance.ReadModel;
    using System.Linq;

    public interface IQueryContext
    {
        public IQueryable<UserEntity> Users { get; }

        public IQueryable<SessionEntity> Sessions { get; }
    }
}
