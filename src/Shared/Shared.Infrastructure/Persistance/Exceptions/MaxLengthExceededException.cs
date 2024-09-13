namespace Kubis1982.Shared.Persistance.Exceptions
{
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
