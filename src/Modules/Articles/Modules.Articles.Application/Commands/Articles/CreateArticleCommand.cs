namespace ModularMonolith.Modules.Articles.Commands.Articles
{
    using System.Threading;
    using System.Threading.Tasks;

    public record CreateArticleCommand(string Code, string Name, string Unit) : EntityCommand
    {
        public string? Description { get; set; }
        internal class CreateArticleCommandHandler(IArticleRepository articleRepository) : EntityCommandHandler<CreateArticleCommand>
        {
            public override async Task<EntityIdentityResult> Handle(CreateArticleCommand argument, CancellationToken cancellationToken)
            {
                ArticleName name = ArticleName.Of(argument.Name);
                ArticleCode code = ArticleCode.Of(argument.Code);
                MeasurementUnitCode unit = MeasurementUnitCode.Of(argument.Unit);
                Article article = Article.Create(name, code, unit, argument.Description);
                await articleRepository.AddAsync(article, cancellationToken);
                return EntityIdentityResult.Create(article);
            }
        }
    }
}
