namespace Kubis1982.Shared.Events.Domain
{
    using Kubis1982.Shared.Kernel;
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
