namespace ModularMonolith.Modules.ReadModel.Annotations {
    using Microsoft.AspNetCore.Mvc;
    using ModularMonolith.Modules.ReadModel.Exceptions;
    using System;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ControllerRouteAttribute(Modules modules) : RouteAttribute($"/{ServiceCollectionExtensions.MODULE_CODE.ToLower()}/{modules.GetModulePrefixFromEnumValue()}") {
    }
}
