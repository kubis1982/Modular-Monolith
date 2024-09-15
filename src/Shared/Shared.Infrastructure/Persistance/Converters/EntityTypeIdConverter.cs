namespace ModularMonolith.Shared.Persistance.Converters
{
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
    using ModularMonolith.Shared.Kernel.Types;

    internal class EntityTypeIdConverter : ValueConverter<EntityTypeId, string>
    {
        public EntityTypeIdConverter() : base(
               code => code.Value,
               code => string.IsNullOrEmpty(code) ? EntityTypeId.Empty : new EntityTypeId(code))
        {
        }
    }
}
