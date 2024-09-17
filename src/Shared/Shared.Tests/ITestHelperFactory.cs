namespace ModularMonolith.Shared
{
    using Microsoft.Extensions.DependencyInjection;
    using System;

    /// <summary>
    /// Represents a factory for creating test helpers.
    /// </summary>
    public interface ITestHelperFactory
    {
        /// <summary>
        /// Gets or sets the log action for the test helper.
        /// </summary>
        Action<string>? Log { get; set; }

        /// <summary>
        /// Creates a test helper.
        /// </summary>
        /// <param name="migration">Indicates whether to perform database migration.</param>
        /// <param name="serviceCollection">An optional action to configure the service collection.</param>
        /// <returns>The created test helper.</returns>
        TestHelper CreateTestHelper(bool migration, Action<IServiceCollection>? serviceCollection = null);
    }
}
