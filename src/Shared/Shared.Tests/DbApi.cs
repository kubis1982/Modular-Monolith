namespace ModularMonolith.Shared
{
    using Microsoft.Extensions.DependencyInjection;
    using ModularMonolith.Shared.Persistance.ReadModel;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    internal class DbApi<TReadDbContext>(IServiceProvider serviceProvider) : IDbApi where TReadDbContext : ReadDbContextBase
    {
        private readonly IServiceProvider serviceProvider = serviceProvider;

        public IDbContext CreateDbContext()
        {
            return new DbContext<TReadDbContext>(serviceProvider);
        }

        public void Add<TEntity>(params TEntity[] entities) where TEntity : class
        {
            using var scope = serviceProvider.CreateScope();
            using var db = scope.ServiceProvider.GetRequiredService<TReadDbContext>();
            db.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.TrackAll;
            db.AddRange(entities);
            db.SaveChanges();
        }

        public void Update<TEntity>(Expression<Func<TEntity, bool>> expression, Action<TEntity> action) where TEntity : class
        {
            using var scope = serviceProvider.CreateScope();
            using var db = scope.ServiceProvider.GetRequiredService<TReadDbContext>();
            db.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.TrackAll;
            TEntity entity = db.Set<TEntity>().Single(expression);
            action.Invoke(entity);
            db.SaveChanges();
        }

        public TEntity GetSingle<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class
        {
            using var scope = serviceProvider.CreateScope();
            using var db = scope.ServiceProvider.GetRequiredService<TReadDbContext>();
            return db.Set<TEntity>().Single(expression);
        }

        public TEntity? GetSingleOrDefault<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class
        {
            using var scope = serviceProvider.CreateScope();
            using var db = scope.ServiceProvider.GetRequiredService<TReadDbContext>();
            return db.Set<TEntity>().SingleOrDefault(expression);
        }

        public TEntity[] GetArray<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class
        {
            using var scope = serviceProvider.CreateScope();
            using var db = scope.ServiceProvider.GetRequiredService<TReadDbContext>();
            return db.Set<TEntity>().Where(expression).ToArray();
        }

        public bool Any<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class
        {
            using var scope = serviceProvider.CreateScope();
            using var db = scope.ServiceProvider.GetRequiredService<TReadDbContext>();
            return db.Set<TEntity>().Any(expression);
        }
    }
}
