namespace ModularMonolith.Modules.Articles.Domain
{
    public sealed class EntityType : EntityTypeEnumerator
    {
        public const string ModuleCode = "ArM";

        private EntityType(short numerator, string name) : base(ModuleCode, numerator, name)
        {
        }
        public static EntityType Article => new(1, "Artykuł");
        public static EntityType MeasurementUnit => new(2, "Jednostka miary");
        public static EntityType ArticleUnit => new(3, "Jednostka artykułu");
        public static EntityType Group => new(4, "Grupa artykułu");
    }
}