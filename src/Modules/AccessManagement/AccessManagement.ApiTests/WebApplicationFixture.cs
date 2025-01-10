namespace ModularMonolith.Modules.AccessManagement
{
    using Microsoft.Extensions.DependencyInjection;
    using ModularMonolith.Modules.AccessManagement.Persistance.ReadModel;
    using ModularMonolith.Shared.Security;

    [CollectionDefinition(nameof(WebApplicationFixtureCollection))]
    public class WebApplicationFixtureCollection : ICollectionFixture<WebApplicationFixture> { }

    public class WebApplicationFixture() : Shared.Api.WebApplicationFixture<ReadDbContext>(ModuleDefinition.MODULE_CODE)
    {
        protected override void OnServices(IServiceCollection services)
        {
            base.OnServices(services);

            services.Configure<AuthOptions>(options =>
            {
                options.Issuer = "https://localhost:7778";
                options.Audience = "https://localhost:7778";
                options.SecretKey = "4f1feeca525de4cdb064656007da3edac7895a87ff0ea865693300fb8b6e8f9c";
            });
        }
    }
}
