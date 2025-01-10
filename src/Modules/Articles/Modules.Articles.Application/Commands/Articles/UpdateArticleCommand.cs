namespace ModularMonolith.Modules.Articles.Commands.Articles
{
    using System.Threading;
    using System.Threading.Tasks;

    public sealed record UpdateArticleCommand(int ArticleId, string Code, string Name, string Unit) : UnitOfWorkCommand
    {
        public string? Description { get; init; }
        internal class UpdateArticleCommandHandler(IArticleRepository articleRepository, IArticleUsageService articleUsageService) : UnitOfWorkCommandHandler<UpdateArticleCommand>
        {
            public override async Task Handle(UpdateArticleCommand command, CancellationToken cancellationToken)
            {
                Article article = await articleRepository.SingleAsync(ArticleSpec.ById(command.ArticleId), cancellationToken);
                article.Update(articleUsageService, ArticleCode.Of(command.Code), ArticleName.Of(command.Name), MeasurementUnitCode.Of(command.Unit), command.Description ?? string.Empty);
            }
        }
    }
}
