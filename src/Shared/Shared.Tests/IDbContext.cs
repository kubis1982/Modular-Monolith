namespace ModularMonolith.Shared
{
    using System.Linq.Expressions;

    public interface IDbContext : IDisposable
    {
        void AddRange<TEntity>(params TEntity[] entities) where TEntity : class;
        TEntity[] GetArray<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class;
        TEntity GetSingle<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class;
        TEntity? GetSingleOrDefault<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class;
        bool Any<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class;
        int SaveChanges();
    }
}
