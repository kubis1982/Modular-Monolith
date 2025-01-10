using Microsoft.EntityFrameworkCore;
using ModularMonolith.Modules.ReadModel.Persistance.ReadModel.Articles;
using ModularMonolith.Modules.ReadModel.Persistance.ReadModel.Contractors;
using ModularMonolith.Modules.ReadModel.Queries.Articles;
using ModularMonolith.Shared;
using Xunit.Abstractions;

namespace ModularMonolith.Modules.ReadModel.Endpoints {

    public class ArticleTests(WebApplicationFixture webApplicationFixture, ITestOutputHelper testOutputHelper) : ModuleApiTests(webApplicationFixture, testOutputHelper) {
        [Theory]
        [InlineDataFixture]
        public async Task ShouldGetArticle(ArticleEntity articleEntity) {
            // Arrange
            DbContext.Add(articleEntity);
            DbContext.SaveChanges();

            // Act
            var result = await HttpClient.GetAsync<GetArticleQueryResult>($"/arm/articles/{articleEntity.Id}");

            // Assert
            result.Should().NotBeNull();
        }
    }
}
