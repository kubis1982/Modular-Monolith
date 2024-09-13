namespace Kubis1982.Shared.Events.Domain
{
    using Kubis1982.Shared.Kernel;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class DomainEventsDispatcher(IDomainEventPublisher domainEventPublisher) : IDomainEventsDispatcher
    {
        public async Task DispatchEvents(IEnumerable<IDomainEvent> events, CancellationToken cancellationToken)
        {
            var tasks = events
                .Select(async (domainEvent) =>
                {
                    await domainEventPublisher.Publish(domainEvent, cancellationToken);
                });
            await Task.WhenAll(tasks);
        }
    }
}
