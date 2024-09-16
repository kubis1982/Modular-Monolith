namespace ModularMonolith.Shared
{
    using Microsoft.Extensions.DependencyInjection;
    using ModularMonolith.Shared.Persistance.ReadModel;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    internal class DbContext<TReadDbContext> : IDbContext where TReadDbContext : ReadDbContextBase
    {
        private readonly IServiceScope scope;
        private readonly TReadDbContext db;

        public DbContext(IServiceProvider serviceProvider)
        {
            scope = serviceProvider.CreateScope();
            db = scope.ServiceProvider.GetRequiredService<TReadDbContext>();
            db.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.TrackAll;
        }

        public void Dispose()
        {
            db?.Dispose();
            scope?.Dispose();
        }


        public void AddRange<TEntity>(params TEntity[] entities) where TEntity : class => db.AddRange(entities);

        public TEntity GetSingle<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class => db.Set<TEntity>().Single(expression);

        public TEntity? GetSingleOrDefault<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class => db.Set<TEntity>().SingleOrDefault(expression);

        public TEntity[] GetArray<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class => db.Set<TEntity>().Where(expression).ToArray();

        public bool Any<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class => db.Set<TEntity>().Any(expression);

        public int SaveChanges() => db.SaveChanges();
    }
}
