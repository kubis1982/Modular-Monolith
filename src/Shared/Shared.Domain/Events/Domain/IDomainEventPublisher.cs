namespace ModularMonolith.Shared.Events.Domain
{
    using ModularMonolith.Shared.Kernel;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IDomainEventPublisher
    {
        Task Publish(IDomainEvent domainEvent, CancellationToken cancellationToken = default);
    }
}
