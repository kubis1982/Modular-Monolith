namespace ModularMonolith.Shared.Persistance.Exceptions
{
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class NumericOverflowException : DbUpdateException
    {
        public NumericOverflowException() : base("Przepełnienie numeryczne")
        {
        }

        public NumericOverflowException(Exception? innerException) : base("Przepełnienie numeryczne", innerException)
        {
        }

        public NumericOverflowException(IReadOnlyList<EntityEntry> entries) : base("Przepełnienie numeryczne", entries)
        {
        }

        public NumericOverflowException(Exception? innerException, IReadOnlyList<EntityEntry> entries) : base("Przepełnienie numeryczne", innerException, entries)
        {
        }
    }
}
