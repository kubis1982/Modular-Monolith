namespace ModularMonolith.Modules.Contractors.Domain
{
    public sealed class EntityType : EntityTypeEnumerator
    {
        public const string ModuleCode = "CnM";

        private EntityType(short numerator, string name) : base(ModuleCode, numerator, name)
        {
        }

        public static EntityType Contractor => new(1, "Kontrahent");
        public static EntityType Address => new(2, "Adres kontrahenta");
        public static EntityType Country => new(3, "Kraj");
    }
}
