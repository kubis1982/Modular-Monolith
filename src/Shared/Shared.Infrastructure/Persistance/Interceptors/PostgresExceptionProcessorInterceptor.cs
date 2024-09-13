namespace Kubis1982.Shared.Persistance.Interceptors
{
    using Kubis1982.Shared.Persistance.Interceptors.Abstracts;
    using Npgsql;

    internal class PostgresExceptionProcessorInterceptor : ExceptionProcessorInterceptor<PostgresException>
    {
        protected override DatabaseError? GetDatabaseError(PostgresException dbException)
        {
            return dbException.SqlState switch
            {
                PostgresErrorCodes.StringDataRightTruncation => DatabaseError.MaxLength,
                PostgresErrorCodes.NumericValueOutOfRange => DatabaseError.NumericOverflow,
                PostgresErrorCodes.NotNullViolation => DatabaseError.CannotInsertNull,
                PostgresErrorCodes.UniqueViolation => DatabaseError.UniqueConstraint,
                PostgresErrorCodes.ForeignKeyViolation => DatabaseError.ReferenceConstraint,
                _ => null
            };
        }
    }
}
