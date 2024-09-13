namespace Kubis1982.Shared.Persistance.Exceptions
{
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CannotInsertNullException : DbUpdateException
    {
        public CannotInsertNullException() : base("Nie można wstawić wartości null")
        {
        }

        public CannotInsertNullException(Exception? innerException) : base("Nie można wstawić wartości null", innerException)
        {
        }

        public CannotInsertNullException(IReadOnlyList<EntityEntry> entries) : base("Nie można wstawić wartości null", entries)
        {
        }

        public CannotInsertNullException(Exception? innerException, IReadOnlyList<EntityEntry> entries) : base("Nie można wstawić wartości nulll", innerException, entries)
        {
        }
    }
}
