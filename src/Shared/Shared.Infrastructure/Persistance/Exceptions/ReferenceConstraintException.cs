﻿namespace Kubis1982.Shared.Persistance.Exceptions
{
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
