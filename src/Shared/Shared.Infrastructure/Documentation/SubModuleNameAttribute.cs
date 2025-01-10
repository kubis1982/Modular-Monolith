namespace ModularMonolith.Shared.Documentation {
    using System;

    [AttributeUsage(AttributeTargets.Assembly, Inherited = true)]
    public abstract class SubModuleNameAttribute : Attribute {
        public abstract string? GetName(Type type);
    }
}
