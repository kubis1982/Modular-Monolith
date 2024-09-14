namespace ModularMonolith.Shared.Kernel.Types
{
    using ModularMonolith.Shared.Kernel;
    using System;
    using System.Collections.Generic;

    public abstract class DomainEntity<TEntityId, TKey> : IDomainEntity where TEntityId : EntityId<TKey>
    {    
        public TEntityId Id { get; protected set; } = null!;

        protected DomainEntity()
        {
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as DomainEntity<TEntityId, TKey>);
        }

        public bool Equals(DomainEntity<TEntityId, TKey>? other)
        {
            return other is not null &&
                   EqualityComparer<TEntityId>.Default.Equals(Id, other.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public static bool operator ==(DomainEntity<TEntityId, TKey>? left, DomainEntity<TEntityId, TKey>? right)
        {
            return EqualityComparer<DomainEntity<TEntityId, TKey>>.Default.Equals(left, right);
        }

        public static bool operator !=(DomainEntity<TEntityId, TKey>? left, DomainEntity<TEntityId, TKey>? right)
        {
            return !(left == right);
        }

        #region Events

        IReadOnlyCollection<IDomainEvent> IDomainEntity.Events => Events.AsReadOnly();

        /// <summary>
        /// Clear domain events.
        /// </summary>
        void IDomainEntity.ClearEvents() => Events.Clear();

        /// <summary>
        /// Add domain event.
        /// </summary>
        /// <param name="domainEvent"></param>
        protected void AddEvent(IDomainEvent domainEvent)
        {
            Events.Add(domainEvent);
        }

        private List<IDomainEvent>? events;

        private List<IDomainEvent> Events
        {
            get
            {
                return events ??= [];
            }
        }

        #endregion
    }

    public abstract class DomainEntity<TEntityId, TKey, TEntityTypeEnumerator> : DomainEntity<TEntityId, TKey> where TEntityId : EntityId<TKey, TEntityTypeEnumerator> where TEntityTypeEnumerator : EntityTypeEnumerator
    {
    }
}
