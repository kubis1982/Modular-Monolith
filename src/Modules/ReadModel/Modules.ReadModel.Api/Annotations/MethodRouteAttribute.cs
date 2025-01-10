namespace ModularMonolith.Modules.ReadModel.Annotations {
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Diagnostics.CodeAnalysis;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class MethodRouteAttribute([StringSyntax("Route")] string template) : HttpGetAttribute(template) {
    }
}
