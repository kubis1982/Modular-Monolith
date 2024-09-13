namespace Kubis1982.Shared.Persistance.Interceptors.Abstracts
{
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class ExceptionProcessorInterceptor<T> : SaveChangesInterceptor where T : DbException
    {
        protected abstract DatabaseError? GetDatabaseError(T dbException);

        /// <inheritdoc />
        public override void SaveChangesFailed(DbContextErrorEventData eventData)
        {
            if (eventData.Exception.GetBaseException() is T providerException)
            {
                var error = GetDatabaseError(providerException);

                if (error != null && eventData.Exception is DbUpdateException dbUpdateException)
                {
                    var exception = ExceptionFactory.Create(error.Value, dbUpdateException, dbUpdateException.Entries);
                    throw exception;
                }
            }

            base.SaveChangesFailed(eventData);
        }

        /// <inheritdoc />
        public override Task SaveChangesFailedAsync(DbContextErrorEventData eventData, CancellationToken cancellationToken = new CancellationToken())
        {
            if (eventData.Exception.GetBaseException() is T providerException)
            {
                var error = GetDatabaseError(providerException);

                if (error != null && eventData.Exception is DbUpdateException dbUpdateException)
                {
                    var exception = ExceptionFactory.Create(error.Value, dbUpdateException, dbUpdateException.Entries);
                    throw exception;
                }
            }

            return base.SaveChangesFailedAsync(eventData, cancellationToken);
        }
    }
}
