namespace ModularMonolith.Shared.Time
{
    using System;

    public class Clock : IClock
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
