namespace ModularMonolith.Modules.Orders.Domain.Orders.Exceptions
{
    using System;

    [Serializable]
    public class ArticleBlockedException(Article article) : AppException($"Artykuł `({article.Code}) {article.Name}` jest zablokowany. Nie można go wskazać na dostawie.")
    {
    }
}