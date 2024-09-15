namespace ModularMonolith.Shared.Persistance.Exceptions
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using System;
    using System.Collections.Generic;

    public class ReferenceConstraintException : DbUpdateException
    {
        private const string message = "Nie możesz usunąć obiektu ponieważ jest on wykorzystywany";
        public ReferenceConstraintException() : base(message)
        {
        }

        public ReferenceConstraintException(Exception? innerException) : base(message, innerException)
        {
        }

        public ReferenceConstraintException(Exception? innerException, IReadOnlyList<EntityEntry> entries) : base(message, innerException, entries)
        {
        }
    }
}
