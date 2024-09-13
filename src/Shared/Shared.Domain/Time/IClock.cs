namespace Kubis1982.Shared.Time
{
    using System;

    public interface IClock
    {
        DateTime Now { get; }
    }
}
