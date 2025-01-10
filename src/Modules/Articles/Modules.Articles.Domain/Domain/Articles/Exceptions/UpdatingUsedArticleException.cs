namespace ModularMonolith.Modules.Articles.Domain.Articles.Exceptions
{
    using System;

    [Serializable]
    internal class UpdatingUsedArticleException(string entityName) : AppException($"Nie można zmienić artykułu (jego jednostki miary). (powiązanie z `{entityName}`)")
    {
    }
}