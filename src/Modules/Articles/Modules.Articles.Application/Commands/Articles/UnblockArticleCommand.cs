namespace ModularMonolith.Modules.Articles.Commands.Articles
{

    using System.Threading;
    using System.Threading.Tasks;

    public sealed record UnblockArticleCommand(int ArticleId) : UnitOfWorkCommand
    {
        internal class UnblockArticleCommandHandler(IArticleRepository articleRepository) : UnitOfWorkCommandHandler<UnblockArticleCommand>
        {
            public override async Task Handle(UnblockArticleCommand command, CancellationToken cancellationToken)
            {
                Article article = await articleRepository.SingleAsync(ArticleSpec.ById(command.ArticleId), cancellationToken);
                article.Unblock();
            }
        }
    }
}
