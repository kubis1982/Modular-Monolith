namespace Kubis1982.Shared.Kernel
{
    using System.Collections.Generic;

    public interface IDomainEntity
    {
        IReadOnlyCollection<IDomainEvent> Events { get; }
        void ClearEvents();
    }
}
