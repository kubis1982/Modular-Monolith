namespace ModularMonolith.Modules.ProjectName.Domain
{
    using ModularMonolith.Shared.Kernel.Types;

    public sealed class EntityType : EntityTypeEnumerator
    {
        public const string ModuleCode = "<>";

        private EntityType(short numerator, string name) : base(ModuleCode, numerator, name)
        {
        }

        public static EntityType Entity => new(1, nameof(Entity));
    }
}
