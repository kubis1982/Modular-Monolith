namespace ModularMonolith.Shared
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents the interface for the test database API.
    /// </summary>
    public interface ITestDbApi : ITestDb
    {
        /// <summary>
        /// Updates entities in the database based on the specified expression.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="expression">The expression used to filter the entities to be updated.</param>
        /// <param name="action">The action to be performed on the entities.</param>
        void Update<TEntity>(Expression<Func<TEntity, bool>> expression, Action<TEntity> action) where TEntity : class;

        /// <summary>
        /// Creates a new instance of the test database context.
        /// </summary>
        /// <returns>The test database context.</returns>
        ITestDbContext CreateDbContext();
    }
}
