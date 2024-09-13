namespace Kubis1982.Shared.Events.Domain
{
    using Kubis1982.Shared.Kernel;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IDomainEventsDispatcher
    {
        Task DispatchEvents(IEnumerable<IDomainEvent> events, CancellationToken cancellationToken);
    }
}
