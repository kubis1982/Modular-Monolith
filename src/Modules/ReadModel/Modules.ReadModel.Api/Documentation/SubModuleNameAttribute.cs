using ModularMonolith.Modules.ReadModel.Documentation;

[assembly: SubModuleName]

namespace ModularMonolith.Modules.ReadModel.Documentation {
    using System;

    internal class SubModuleNameAttribute : Shared.Documentation.SubModuleNameAttribute {
        public override string? GetName(Type type) {
            if (type.Name?.EndsWith("Endpoints") == true) {
                string name = type.Name;
                name = name[..name.IndexOf("Endpoints")];
                return name;
            }
            return null;
        }
    }
}
