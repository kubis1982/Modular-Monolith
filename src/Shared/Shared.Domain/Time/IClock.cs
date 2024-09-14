namespace ModularMonolith.Shared.Time
{
    using System;

    public interface IClock
    {
        DateTime Now { get; }
    }
}
