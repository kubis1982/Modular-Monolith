namespace ModularMonolith.Shared
{
    using Microsoft.Extensions.DependencyInjection;
    using ModularMonolith.Shared.Persistance.ReadModel;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    internal class TestDbApi<TReadDbContext>(IServiceProvider serviceProvider) : ITestDbApi where TReadDbContext : ReadDbContextBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestDbApi{TReadDbContext}"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public TestDbApi(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="ITestDbContext"/>.
        /// </summary>
        /// <returns>The created <see cref="ITestDbContext"/>.</returns>
        public ITestDbContext CreateDbContext()
        {
            return new TestDbContext<TReadDbContext>(serviceProvider);
        }

        /// <summary>
        /// Adds the specified entities to the database.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entities.</typeparam>
        /// <param name="entities">The entities to add.</param>
        public void Add<TEntity>(params TEntity[] entities) where TEntity : class
        {
            using var scope = serviceProvider.CreateScope();
            using var db = scope.ServiceProvider.GetRequiredService<TReadDbContext>();
            db.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.TrackAll;
            db.AddRange(entities);
            db.SaveChanges();
        }

        /// <summary>
        /// Updates the entity that matches the specified expression.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="expression">The expression to match the entity.</param>
        /// <param name="action">The action to perform on the entity.</param>
        public void Update<TEntity>(Expression<Func<TEntity, bool>> expression, Action<TEntity> action) where TEntity : class
        {
            using var scope = serviceProvider.CreateScope();
            using var db = scope.ServiceProvider.GetRequiredService<TReadDbContext>();
            db.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.TrackAll;
            TEntity entity = db.Set<TEntity>().Single(expression);
            action.Invoke(entity);
            db.SaveChanges();
        }

        /// <summary>
        /// Gets a single entity that matches the specified expression.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="expression">The expression to match the entity.</param>
        /// <returns>The single entity that matches the expression.</returns>
        public TEntity GetSingle<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class
        {
            using var scope = serviceProvider.CreateScope();
            using var db = scope.ServiceProvider.GetRequiredService<TReadDbContext>();
            return db.Set<TEntity>().Single(expression);
        }

        /// <summary>
        /// Gets a single entity that matches the specified expression, or null if no entity is found.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="expression">The expression to match the entity.</param>
        /// <returns>The single entity that matches the expression, or null if no entity is found.</returns>
        public TEntity? GetSingleOrDefault<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class
        {
            using var scope = serviceProvider.CreateScope();
            using var db = scope.ServiceProvider.GetRequiredService<TReadDbContext>();
            return db.Set<TEntity>().SingleOrDefault(expression);
        }

        /// <summary>
        /// Gets an array of entities that match the specified expression.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entities.</typeparam>
        /// <param name="expression">The expression to match the entities.</param>
        /// <returns>An array of entities that match the expression.</returns>
        public TEntity[] GetArray<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class
        {
            using var scope = serviceProvider.CreateScope();
            using var db = scope.ServiceProvider.GetRequiredService<TReadDbContext>();
            return db.Set<TEntity>().Where(expression).ToArray();
        }

        /// <summary>
        /// Checks if any entity matches the specified expression.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="expression">The expression to match the entity.</param>
        /// <returns>True if any entity matches the expression, otherwise false.</returns>
        public bool Any<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class
        {
            using var scope = serviceProvider.CreateScope();
            using var db = scope.ServiceProvider.GetRequiredService<TReadDbContext>();
            return db.Set<TEntity>().Any(expression);
        }
    }
}
