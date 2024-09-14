namespace ModularMonolith.Shared.Events.Domain
{
    using ModularMonolith.Shared.Kernel;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IDomainEventsDispatcher
    {
        Task DispatchEvents(IEnumerable<IDomainEvent> events, CancellationToken cancellationToken);
    }
}
