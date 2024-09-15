namespace ModularMonolith.Shared.Events.Domain
{
    using MediatR;
    using ModularMonolith.Shared.Kernel;
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
