namespace ModularMonolith.Shared
{
    public class TestHelper : IDisposable
    {
        private readonly HttpTestClient httpTestClient;
        private readonly IDbApi dbApi;

        internal TestHelper(HttpTestClient httpTestClient, IDbApi dbApi, IServiceProvider services)
        {
            this.httpTestClient = httpTestClient;
            this.dbApi = dbApi;
            Services = services;
        }

        public HttpTestClient HttpClient => httpTestClient;

        public IDbApi Db => dbApi;

        public IServiceProvider Services { get; }

        public void Dispose()
        {
            HttpClient.Dispose();
        }
    }
}
