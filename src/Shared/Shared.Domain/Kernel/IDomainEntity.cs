namespace ModularMonolith.Shared.Kernel
{
    using System.Collections.Generic;

    public interface IDomainEntity
    {
        IReadOnlyCollection<IDomainEvent> Events { get; }
        void ClearEvents();
    }
}
