namespace ModularMonolith.Shared.Persistance.Interceptors
{
    using ModularMonolith.Shared.Kernel;
    using ModularMonolith.Shared.Persistance.WriteModel;
    using ModularMonolith.Shared.Security;
    using ModularMonolith.Shared.Time;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    internal class CreatorAndModifierInterceptor(IUserContextAccessor userContextAccessor, IClock clock) : ISaveChangesInterceptor
    {
        public InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            SetUserId(eventData.Context, userContextAccessor.Get().UserId, clock.Now);
            return result;
        }

        public ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            SetUserId(eventData.Context, userContextAccessor.Get().UserId, clock.Now);
            return new(result);
        }

        private static void SetUserId(DbContext? db, int userId, DateTime dt)
        {
            if (db is null)
                return;

            foreach (EntityEntry? entry in db.ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                {
                    continue;
                }

                if (entry.State == EntityState.Added)
                {
                    if (entry.Properties.Any(n => n.Metadata.Name == ShadowProperties.CreatedBy) && userId > 0)
                    {
                        var property = entry.Property(ShadowProperties.CreatedBy);
                        if (property.Metadata.ClrType == typeof(int?) || property.Metadata.ClrType == typeof(int))
                            property.CurrentValue = userId;
                    }
                    if (entry.Properties.Any(n => n.Metadata.Name == ShadowProperties.CreatedOn))
                    {
                        entry.Property(ShadowProperties.CreatedOn).CurrentValue = dt;
                    }
                    continue;
                }

                if (entry.Entity is IDomainEntity domainEntity)
                {
                    if (entry.State == EntityState.Modified)
                    {
                        if (entry.Properties.Any(n => n.Metadata.Name == ShadowProperties.ModifiedBy))
                        {
                            var property = entry.Property(ShadowProperties.ModifiedBy);
                            if (property.Metadata.ClrType == typeof(int?) || property.Metadata.ClrType == typeof(int))
                                property.CurrentValue = userId;
                        }

                        if (entry.Properties.Any(n => n.Metadata.Name == ShadowProperties.ModifiedOn))
                        {
                            entry.Property(ShadowProperties.ModifiedOn).CurrentValue = dt;
                        }
                    }
                }
            }
        }
    }
}
