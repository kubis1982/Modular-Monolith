namespace ModularMonolith.Modules.AccessManagement
{
    using Microsoft.Extensions.DependencyInjection;
    using ModularMonolith.Shared;
    using ModularMonolith.Shared.Security;
    using Xunit.Abstractions;

    public class ApiTests(ITestOutputHelper testOutputHelper) : ApiTests<TestHelperFactory>(testOutputHelper)
    {
        protected override void OnAddServices(IServiceCollection serviceCollection)
        {
            base.OnAddServices(serviceCollection);

            serviceCollection.Configure<AuthOptions>(options =>
            {
                options.Issuer = "https://localhost:8081";
                options.Audience = "https://localhost:8081";
                options.SecretKey = "4f1feeca525de4cdb064656007da3edac7895a87ff0ea865693300fb8b6e8f9c";
            });
        }
    }
}
