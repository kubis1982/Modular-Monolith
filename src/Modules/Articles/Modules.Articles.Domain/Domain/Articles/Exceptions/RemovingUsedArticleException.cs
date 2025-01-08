namespace ModularMonolith.Modules.Articles.Domain.Articles.Exceptions
{
    internal class RemovingUsedArticleException(string entityName) : AppException($"Nie można usunąć artykułu. (powiązanie z `{entityName}`)")
    {
    }
}