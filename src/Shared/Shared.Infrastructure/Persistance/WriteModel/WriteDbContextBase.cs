namespace Kubis1982.Shared.Persistance.WriteModel
{
    using Kubis1982.Shared.Kernel.Types;
    using Kubis1982.Shared.Persistance.Comparers;
    using Kubis1982.Shared.Persistance.Converters;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;

    public abstract class WriteDbContextBase(DbContextOptions options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema(Schema);

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly, n => n.Namespace?.StartsWith(GetType().Namespace!) == true);

            if (EF.IsDesignTime)
            {
                SeedData(modelBuilder);
            }
        }

        protected sealed override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            configurationBuilder
                .Properties<EntityTypeId>()
                .HaveMaxLength(5).AreFixedLength(true).AreUnicode(false)
                .HaveConversion<EntityTypeIdConverter>();

            configurationBuilder
                .Properties<DateTime>()
                .HavePrecision(2);

            configurationBuilder
                .Properties<decimal>()
                .HavePrecision(14, 4);

            configurationBuilder
               .Properties<string>()
               .HaveMaxLength(50);

            configurationBuilder
            .Properties<Dictionary<string, string>>()
            .HaveConversion<DictionaryStringConverter, DictionaryStringComparer>();
        }

        protected abstract string Schema { get; }

        protected virtual void SeedData(ModelBuilder modelBuilder)
        {
        }
    }
}
