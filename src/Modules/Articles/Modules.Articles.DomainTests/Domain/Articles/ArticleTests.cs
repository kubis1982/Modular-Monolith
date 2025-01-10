namespace ModularMonolith.Modules.Articles.Domain.Articles
{
    using ModularMonolith.Modules.Articles.Domain.MeasurementUnits;

    public class ArticleTests : ModuleDomainTests
    {

        [Theory]
        [InlineDataFixture]
        public void ShouldCreateArticle(Article article)
        {
            var @event = article.Extensions().GetEvent<Events.ArticleCreatedEvent>();
            article.IsBlocked.Should().BeFalse();
            @event.ArticleId.Should().Be(article.Id);
            @event.Code.Should().Be(article.Code);
            @event.Name.Should().Be(article.Name);
            @event.Unit.Should().Be(article.Unit);
            @event.Description.Should().Be(article.Description);
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldUpdateArticle(Article article, ArticleCode articleCode, ArticleName articleName, MeasurementUnitCode measurementUnitCode, string? description)
        {
            // Arrange
            Mock<IArticleUsageService> articleUsageService = new();

            // Act
            article.Update(articleUsageService.Object, articleCode, articleName, measurementUnitCode, description);

            // Assert
            var @event = article.Extensions().GetEvent<Events.ArticleUpdatedEvent>();
            @event.ArticleId.Should().Be(article.Id);
            @event.Name.Should().Be(articleName);
            @event.Code.Should().Be(articleCode);
            @event.Unit.Should().Be(measurementUnitCode);
            @event.Description.Should().Be(description);
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldRemoveArticle(Article article)
        {
            // Arrange
            Mock<IArticleUsageService> mock = new();

            // Act
            article.Remove(mock.Object);

            // Assert
            var @event = article.Extensions().GetEvent<Events.ArticleRemovedEvent>();
            @event.ArticleId.Should().Be(article.Id);
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldBlockArticle(Article article)
        {
            article.Block();

            var @event = article.Extensions().GetEvent<Events.ArticleBlockedEvent>();
            article.IsBlocked.Should().BeTrue();
            @event.ArticleId.Should().Be(article.Id);
        }

        [Theory]
        [InlineDataFixture]
        public void ShouldUnblockArticle(Article article)
        {
            article.Extensions().SetValue(n => n.IsBlocked, true);

            article.Unblock();

            var @event = article.Extensions().GetEvent<Events.ArticleUnblockedEvent>();
            article.IsBlocked.Should().BeFalse();
            @event.ArticleId.Should().Be(article.Id);
        }
    }
}
