namespace ModularMonolith.Modules.AccessManagement.Domain
{
    using ModularMonolith.Shared.Kernel.Types;

    public sealed class EntityType : EntityTypeEnumerator
    {
        public const string ModuleCode = "AcM";

        private EntityType(short numerator, string name) : base(ModuleCode, numerator, name)
        {
        }

        public static EntityType User => new(1, nameof(User));

        public static EntityType Session => new(2, nameof(Session));
    }
}
