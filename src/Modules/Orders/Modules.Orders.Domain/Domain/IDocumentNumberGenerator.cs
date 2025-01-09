namespace ModularMonolith.Modules.Ordering.Domain
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IDocumentNumberGenerator
    {
        Task<string> CreateDocumentNumberAsync(EntityType entityType, DateTime dateTime, CancellationToken cancellationToken);
    }
}
