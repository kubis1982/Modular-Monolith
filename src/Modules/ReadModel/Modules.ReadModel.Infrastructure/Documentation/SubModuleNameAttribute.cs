using ModularMonolith.Modules.ReadModel.Documentation;

[assembly: SubModuleName]

namespace ModularMonolith.Modules.ReadModel.Documentation {
    using System;

    internal class SubModuleNameAttribute : Shared.Documentation.SubModuleNameAttribute {
        public override string? GetName(Type type) {
            if (type.Namespace?.StartsWith("ModularMonolith.Modules.ReadModel.Queries.") == true) {
                string @namespace = type.Namespace;
                @namespace = @namespace[("ModularMonolith.Modules.ReadModel.Queries.".Length)..];
                if (@namespace.IndexOf(".") > 0) {
                    @namespace = @namespace[..@namespace.IndexOf(".")];
                }
                return @namespace;
            }
            return null;
        }
    }
}
