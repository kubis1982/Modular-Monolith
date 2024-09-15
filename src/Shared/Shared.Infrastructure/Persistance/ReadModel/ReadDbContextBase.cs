namespace ModularMonolith.Shared.Persistance.ReadModel
{
    using Microsoft.EntityFrameworkCore;
    using ModularMonolith.Shared.Kernel.Types;
    using ModularMonolith.Shared.Persistance.Converters;
    using System.Collections.Generic;

    public class ReadDbContextBase(DbContextOptions options) : DbContext(options)
    {
        protected sealed override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            configurationBuilder
                .Properties<EntityTypeId>()
                .AreFixedLength(true)
                .HaveConversion<EntityTypeIdConverter>();

            configurationBuilder
                .Properties<Dictionary<string, string>>()
                .HaveConversion<DictionaryStringConverter>();
        }
    }
}
