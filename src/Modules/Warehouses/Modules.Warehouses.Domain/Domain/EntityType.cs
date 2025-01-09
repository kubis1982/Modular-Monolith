namespace ModularMonolith.Modules.Warehouses.Domain
{
    public sealed class EntityType : EntityTypeEnumerator
    {
        public const string ModuleCode = "WaM";

        private EntityType(short numerator, string name) : base(ModuleCode, numerator, name)
        {
        }
        public static EntityType Warehouse => new(1, "Magazyn");
        public static EntityType Schema => new(2, "Schemat magazynu");
        public static EntityType Location => new(3, "Lokalizacja magazynowa");
    }
}