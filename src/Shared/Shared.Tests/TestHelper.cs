namespace ModularMonolith.Shared
{
    /// <summary>
    /// Helper class for testing purposes.
    /// </summary>
    public class TestHelper(TestHttpClient httpClient, ITestDbApi db, IServiceProvider services) : IDisposable
    {
        /// <summary>
        /// Gets the instance of the TestHttpClient.
        /// </summary>
        public TestHttpClient HttpClient { get; } = httpClient;

        /// <summary>
        /// Gets the instance of the IDbApi.
        /// </summary>
        public ITestDbApi Db { get; } = db;

        /// <summary>
        /// Gets the instance of the IServiceProvider.
        /// </summary>
        public IServiceProvider Services { get; } = services;

#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
        /// <summary>
        /// Disposes the TestHttpClient instance.
        /// </summary>
        void IDisposable.Dispose() => HttpClient.Dispose();
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
    }
}
