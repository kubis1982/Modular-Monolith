using ModularMonolith.Modules.Articles.Domain.Articles;
using ModularMonolith.Modules.Articles.Domain.MeasurementUnits;
using System.Globalization;

namespace ModularMonolith.Modules.Articles.Customizations
{

    internal class ArticleCustomisation : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<Article>(n => n.FromFactory(() =>
              Article.Create(fixture.Create<string>(), fixture.Create<ArticleCode>(), fixture.Create<MeasurementUnitCode>(), fixture.Create<string>())
                  .Extensions().SetValue(n => n.Id, new ArticleId(fixture.Create<int>())).DomainEntity));

            fixture.Register(() => CultureInfo.GetCultures(CultureTypes.NeutralCultures).OrderBy(c => Guid.NewGuid()).First());
            fixture.Customize<ArticleCode>(n => n.FromFactory(() =>
            ArticleCode.Of((string)fixture.Create(
                new RegularExpressionRequest("^" + ArticleCode.Pattern + "$"),
                new SpecimenContext(fixture)))));
        }
    }
}
