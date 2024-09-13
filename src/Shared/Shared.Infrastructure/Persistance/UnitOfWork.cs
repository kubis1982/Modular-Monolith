namespace Kubis1982.Shared.Persistance
{
    using Kubis1982.Shared.Events.Domain;
    using Kubis1982.Shared.Kernel;
    using Kubis1982.Shared.Persistance.WriteModel;
    using Kubis1982.Shared.Security;
    using Kubis1982.Shared.Serialization;
    using Kubis1982.Shared.Time;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class UnitOfWork<T>(T dbContext, IDomainEventsDispatcher domainEventsDispatcher) : IUnitOfWork where T : WriteDbContextBase
    {

        /// <summary>
        /// Pobiera wszystkie zdarzenia domenowe z encji uwzględnionych w kontekście bazodanowym <seealso cref="IDomainEvent"/>
        /// </summary>
        /// <returns></returns>
        private IEnumerable<IDomainEvent> GetEvents()
        {
            var domainEntities = dbContext.ChangeTracker
               .Entries<IDomainEntity>().ToList();
            IEnumerable<IDomainEvent> domainEvents = domainEntities
                .SelectMany(entity => entity.Entity.Events)
                .ToList();
            domainEntities.ToList().ForEach(entity => entity.Entity.ClearEvents());
            return domainEvents;
        }

        /// <summary>
        /// Rozpoczyna transakcję SQL
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task BeginTransaction(CancellationToken cancellationToken)
        {
            await dbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        /// <summary>
        /// Zatwierdza transakcję SQL
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task CommitTransaction(CancellationToken cancellationToken)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
            await dbContext.Database.CommitTransactionAsync(cancellationToken);
        }

        /// <summary>
        /// Wykonuje procedury obsługi zdarzeń domenowych
        /// </summary>
        /// <param name="domainEvents"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task DispatchEvents(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken)
        {
            await domainEventsDispatcher.DispatchEvents(domainEvents, cancellationToken);
        }

        /// <summary>
        /// Zatwierdza zmiany w bazie danych. Jeżeli operacja wykryje dodatkowe zdarzenia domenowe wówczas ponawia wykonanie metody.
        /// </summary>
        /// <param name="domainEvents"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<int> SaveChanges(List<IDomainEvent> domainEvents, CancellationToken cancellationToken)
        {
            var events = GetEvents();
            int count = await dbContext.SaveChangesAsync(cancellationToken);
            domainEvents.AddRange(events);
            await DispatchEvents(events, cancellationToken);
            if (events.Any())
            {
                await SaveChanges(domainEvents, cancellationToken);
            }
            return count;
        }

        /// <summary>
        /// Zatwierdza zmiany w kontekście bazodanowym
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<int> Commit(CancellationToken cancellationToken)
        {
            var domainEvents = new List<IDomainEvent>();
            await BeginTransaction(cancellationToken);
            int count = await SaveChanges(domainEvents, cancellationToken);
            //var integrationEvents = dbContext.ConvertDomainToIntegrationEvents(userContext, clock, jsonSerializer, domainEvents).ToArray();
            //var messages = eventRegister.AddOutboxMessages(dbContext, integrationEvents).ToArray();
            await CommitTransaction(cancellationToken);
            //await messageDispatcher.DispatchAsync(messages, cancellationToken);
            return count;
        }
    }
}
