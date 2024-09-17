namespace ModularMonolith.Shared
{
    using Microsoft.Extensions.DependencyInjection;
    using ModularMonolith.Shared.Persistance.ReadModel;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    internal class TestDbContext<TReadDbContext> : ITestDbContext where TReadDbContext : ReadDbContextBase
    {
        /// <summary>
        /// Represents the service scope for dependency injection.
        /// </summary>
        private readonly IServiceScope scope;

        /// <summary>
        /// Represents the instance of the read database context.
        /// </summary>
        private readonly TReadDbContext db;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestDbContext{TReadDbContext}"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider for dependency injection.</param>
        public TestDbContext(IServiceProvider serviceProvider)
        {
            scope = serviceProvider.CreateScope();
            db = scope.ServiceProvider.GetRequiredService<TReadDbContext>();
            db.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.TrackAll;
        }

        /// <summary>
        /// Releases all resources used by the <see cref="TestDbContext{TReadDbContext}"/>.
        /// </summary>
        public void Dispose()
        {
            db?.Dispose();
            scope?.Dispose();
        }

        /// <summary>
        /// Adds one or more entities to the database.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entities">The entities to add.</param>
        public void Add<TEntity>(params TEntity[] entities) where TEntity : class => db.AddRange(entities);

        /// <summary>
        /// Gets a single entity from the database based on the specified expression.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="expression">The expression used to filter the entity.</param>
        /// <returns>The single entity that matches the specified expression.</returns>
        public TEntity GetSingle<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class => db.Set<TEntity>().Single(expression);

        /// <summary>
        /// Gets a single entity from the database based on the specified expression, or null if no entity is found.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="expression">The expression used to filter the entity.</param>
        /// <returns>The single entity that matches the specified expression, or null if no entity is found.</returns>
        public TEntity? GetSingleOrDefault<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class => db.Set<TEntity>().SingleOrDefault(expression);

        /// <summary>
        /// Gets an array of entities from the database based on the specified expression.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="expression">The expression used to filter the entities.</param>
        /// <returns>An array of entities that match the specified expression.</returns>
        public TEntity[] GetArray<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class => db.Set<TEntity>().Where(expression).ToArray();

        /// <summary>
        /// Determines whether any entity exists in the database based on the specified expression.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="expression">The expression used to filter the entities.</param>
        /// <returns>True if any entity exists that matches the specified expression; otherwise, false.</returns>
        public bool Any<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class => db.Set<TEntity>().Any(expression);

        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        public int SaveChanges() => db.SaveChanges();
    }
}
