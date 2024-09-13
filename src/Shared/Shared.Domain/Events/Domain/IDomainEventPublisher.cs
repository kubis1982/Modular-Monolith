namespace Kubis1982.Shared.Events.Domain
{
    using Kubis1982.Shared.Kernel;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IDomainEventPublisher
    {
        Task Publish(IDomainEvent domainEvent, CancellationToken cancellationToken = default);
    }
}
