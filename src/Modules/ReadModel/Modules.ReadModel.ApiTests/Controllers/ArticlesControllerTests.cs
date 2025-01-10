using Microsoft.Extensions.DependencyInjection;
using ModularMonolith.Shared.CQRS.Queries;
using Xunit.Abstractions;

namespace ModularMonolith.Modules.ReadModel.Controllers
{
    public class ArticlesControllerTests(WebApplicationFixture webApplicationFixture, ITestOutputHelper testOutputHelper) : ModuleApiTests(webApplicationFixture, testOutputHelper) {
        private async Task Test<T>(Func<ArticlesController, Task<IQueryable<T>>> func) {
            // Arrange
            IQueryExecutor queryExecutor = Services.GetRequiredService<IQueryExecutor>();
            ArticlesController controller = new(queryExecutor);
            var queryable = await func.Invoke(controller);

            // Act
            var array = queryable.ToArray();

            // Assert
            array.Should().NotBeNull();
        }

        [Fact]
        public async Task GetArticlesAsync() => await Test(n => n.GetArticles(default));
    }
}
