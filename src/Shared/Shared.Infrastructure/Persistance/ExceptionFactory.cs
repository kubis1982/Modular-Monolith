namespace ModularMonolith.Shared.Persistance
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using ModularMonolith.Shared.Persistance.Exceptions;
    using System;
    using System.Collections.Generic;

    static class ExceptionFactory
    {
        internal static Exception Create(DatabaseError error, DbUpdateException exception, IReadOnlyList<EntityEntry> entries)
        {
            return error switch
            {
                DatabaseError.CannotInsertNull when entries.Count > 0 => new CannotInsertNullException(exception.InnerException, entries),
                DatabaseError.CannotInsertNull when entries.Count == 0 => new CannotInsertNullException(exception.InnerException),
                DatabaseError.MaxLength when entries.Count > 0 => new MaxLengthExceededException(exception.InnerException, entries),
                DatabaseError.MaxLength when entries.Count == 0 => new MaxLengthExceededException(exception.InnerException),
                DatabaseError.NumericOverflow when entries.Count > 0 => new NumericOverflowException(exception.InnerException, entries),
                DatabaseError.NumericOverflow when entries.Count == 0 => new NumericOverflowException(exception.InnerException),
                DatabaseError.ReferenceConstraint when entries.Count > 0 => new ReferenceConstraintException(exception.InnerException, entries),
                DatabaseError.ReferenceConstraint when entries.Count == 0 => new ReferenceConstraintException(exception.InnerException),
                DatabaseError.UniqueConstraint when entries.Count > 0 => new UniqueConstraintException(exception.InnerException, entries),
                DatabaseError.UniqueConstraint when entries.Count == 0 => new UniqueConstraintException(exception.InnerException),
                _ => exception,
            };
        }
    }
}
