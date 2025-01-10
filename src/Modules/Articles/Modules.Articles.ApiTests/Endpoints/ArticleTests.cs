using ModularMonolith.Modules.Articles.Persistance.ReadModel;
using ModularMonolith.Modules.Articles.Requests.Articles;
using Xunit.Abstractions;

namespace ModularMonolith.Modules.Articles.Endpoints
{
    public class ArticleTests(WebApplicationFixture webApplicationFixture, ITestOutputHelper testOutputHelper) : ModuleApiTests(webApplicationFixture, testOutputHelper)
    {
        [Theory]
        [InlineDataFixture]
        public async Task ShouldCreateArticle(CreateArticleRequest request)
        {
            // Act
            var identity = await HttpClient.PostAndReturnIdentityAsync($"/articles", request);

            // Assert
            var article = DbContext.Articles.Single(n => n.Id == identity.Id);
            article.Name.Should().Be(request.Name);
            article.Code.Should().Be(request.Code);
            article.Unit.Should().Be(request.Unit);
            article.Description.Should().Be(request.Description);
            article.IsBlocked.Should().BeFalse();
        }

        [Theory]
        [InlineDataFixture]
        public async Task ShouldUpdateArticle(int articleId, ArticleEntity article, UpdateArticleRequest request)
        {
            // Arrange
            article.Id = articleId;
            DbContext.Add(article);
            DbContext.SaveChanges();

            // Act
            await HttpClient.PutAsync($"/articles/{article.Id}", request);

            // Assert
            article = DbContext.Articles.Single(n => n.Id == articleId);
            article.Name.Should().Be(request.Name);
            article.Code.Should().Be(request.Code);
            article.Unit.Should().Be(request.Unit);
            article.Description.Should().Be(request.Description);
            article.IsBlocked.Should().BeFalse();
        }

        [Theory]
        [InlineDataFixture]
        public async Task ShouldBlockArticleAsync(ArticleEntity article)
        {
            // Arrange
            article.IsBlocked = false;
            DbContext.Add(article);
            DbContext.SaveChanges();

            // Act
            await HttpClient.PatchAsync($"/articles/{article.Id}/block");

            // Assert
            DbContext.Articles.Single(n => n.Id == article.Id).IsBlocked.Should().BeTrue();
        }

        [Theory]
        [InlineDataFixture]
        public async Task ShouldUnblockArticleAsync(ArticleEntity article)
        {
            // Arrange
            article.IsBlocked = true;
            DbContext.Add(article);
            DbContext.SaveChanges();

            // Act
            await HttpClient.PatchAsync($"/articles/{article.Id}/unblock");

            // Assert
            DbContext.Articles.Single(n => n.Id == article.Id).IsBlocked.Should().BeFalse();
        }

        [Theory]
        [InlineDataFixture]
        public async Task ShouldDeleteArticle(ArticleEntity article)
        {
            // Arrange
            DbContext.Add(article);
            DbContext.SaveChanges();

            // Act
            await HttpClient.DeleteAndEnsureNoContentAsync($"/articles/{article.Id}");

            // Assert
            DbContext.Articles.Any(n => n.Id == article.Id).Should().BeFalse();
        }
    }
}
