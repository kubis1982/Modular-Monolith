namespace ModularMonolith.Shared.Persistance.Exceptions
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using System;
    using System.Collections.Generic;

    public class MaxLengthExceededException : DbUpdateException
    {
        public MaxLengthExceededException() : base("Przekroczono maksymalną długość")
        {
        }

        public MaxLengthExceededException(Exception? innerException) : base("Przekroczono maksymalną długość", innerException)
        {
        }

        public MaxLengthExceededException(string message, IReadOnlyList<EntityEntry> entries) : base("Przekroczono maksymalną długość", entries)
        {
        }

        public MaxLengthExceededException(Exception? innerException, IReadOnlyList<EntityEntry> entries) : base("Przekroczono maksymalną długość", innerException, entries)
        {
        }
    }
}
