namespace ModularMonolith.Modules.Articles.Commands.Articles
{

    using AutoFixture;
    using AutoFixture.Kernel;
    using ModularMonolith.Modules.Articles.Domain;
    using ModularMonolith.Modules.Articles.Persistance;
    using ModularMonolith.Modules.Articles.Persistance.ReadModel;
    using ModularMonolith.Modules.Articles.Requests.Articles;

    internal class ArticleCustomisation : ICustomization
    {

        public void Customize(IFixture fixture)
        {
            var code = (string)fixture.Create(
                new RegularExpressionRequest(ArticleRestriction.CodePattern),
                new SpecimenContext(fixture));

            fixture.Customize<ArticleEntity>(n => n.With(m => m.TypeId, EntityType.Article.Code.Value)
                .With(m => m.Code, code)
                .With(m => m.Unit, "kg")
                .With(m => m.IsBlocked, false));

            fixture.Customize<CreateArticleRequest>(n => n
                .With(m => m.Unit, "kg")
                .With(m => m.Code, (string)fixture.Create(
                    new RegularExpressionRequest(ArticleRestriction.CodePattern),
                    new SpecimenContext(fixture))));

            fixture.Customize<UpdateArticleRequest>(n => n
                .With(m => m.Unit, "kg")
                .With(m => m.Code, (string)fixture.Create(
                    new RegularExpressionRequest(ArticleRestriction.CodePattern),
                    new SpecimenContext(fixture))));
        }
    }
}