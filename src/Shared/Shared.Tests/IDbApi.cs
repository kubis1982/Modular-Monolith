namespace ModularMonolith.Shared
{
    using System;
    using System.Linq.Expressions;

    public interface IDbApi
    {
        void Add<TEntity>(params TEntity[] entities) where TEntity : class;
        TEntity[] GetArray<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class;
        TEntity GetSingle<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class;
        TEntity? GetSingleOrDefault<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class;
        bool Any<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class;
        void Update<TEntity>(Expression<Func<TEntity, bool>> expression, Action<TEntity> action) where TEntity : class;
        IDbContext CreateDbContext();
    }
}
