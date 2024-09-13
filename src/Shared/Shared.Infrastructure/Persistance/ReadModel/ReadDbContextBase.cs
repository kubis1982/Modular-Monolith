namespace Kubis1982.Shared.Persistance.ReadModel
{
    using Kubis1982.Shared.Kernel.Types;
    using Kubis1982.Shared.Persistance.Converters;
    using Microsoft.EntityFrameworkCore;
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
