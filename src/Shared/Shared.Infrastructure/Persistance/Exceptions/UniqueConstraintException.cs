namespace Kubis1982.Shared.Persistance.Exceptions
{
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
