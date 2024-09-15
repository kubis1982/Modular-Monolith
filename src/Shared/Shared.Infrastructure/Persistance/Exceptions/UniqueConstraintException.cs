namespace ModularMonolith.Shared.Persistance.Exceptions
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using System;
    using System.Collections.Generic;

    public class UniqueConstraintException : DbUpdateException
    {
        public UniqueConstraintException(Exception? innerException) : base("Wskazany obiekt już istnieje w systemie!", innerException)
        {
        }

        public UniqueConstraintException(Exception? innerException, IReadOnlyList<EntityEntry> entries) : base("Wskazany obiekt już istnieje w systemie!", innerException, entries)
        {
        }
    }
}
