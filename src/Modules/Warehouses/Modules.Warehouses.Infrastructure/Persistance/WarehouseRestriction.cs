namespace ModularMonolith.Modules.Warehouses.Persistance
{
    public static class WarehouseRestriction
    {
        public const int CodeLength = 40;
        public const int NameLength = 80;
        public const int DescriptionLength = 256;
        public const string CodePattern = Domain.Warehouses.WarehouseCode.Pattern;
    }
}
