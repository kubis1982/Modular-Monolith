namespace Kubis1982.Modules.AccessManagement.Domain
{
    using Kubis1982.Shared.Kernel.Types;

    public sealed class EntityType : EntityTypeEnumerator
    {
        public const string ModuleCode = "AcM";

        private EntityType(short numerator, string name) : base(ModuleCode, numerator, name)
        {
        }

        public static EntityType User => new(1, "Użytkownik");
    }
}
