namespace ModularMonolith.Shared.Events.Domain
{
    using ModularMonolith.Shared.Kernel;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    internal class DomainEventPublisher(IMediator mediator) : IDomainEventPublisher
    {
        public Task Publish(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
        {
            return mediator.Publish(domainEvent, cancellationToken);
        }
    }
}
