namespace ModularMonolith.Modules.ReadModel.Customizations {
    using AutoFixture;
    using ModularMonolith.Modules.ReadModel.Persistance.ReadModel.Articles;

    internal class ArticlesCustomization : ICustomization {
        public void Customize(IFixture fixture) {
            fixture.Customize<ArticleEntity>(n => n
                .With(m => m.IsBlocked, false));
        }
    }
}
