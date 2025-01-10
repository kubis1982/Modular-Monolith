namespace ModularMonolith.Modules.ReadModel.Annotations
{
    using Microsoft.AspNetCore.Http.Metadata;
    using Microsoft.AspNetCore.Routing;
    using System;
    using System.Collections.Generic;

    public class DocumentationAttribute(Modules module, string operationId, string? description = null) : Attribute, IEndpointSummaryMetadata, IEndpointNameMetadata, ITagsMetadata, IEndpointDescriptionMetadata
    {
        public IReadOnlyList<string> Tags { get; } = ["ReadModel", module.ToString()];

        public string EndpointName { get; } = $"ReadModel{module}{operationId}";

        public string Summary { get; } = $"ReadModel{module}{operationId}";

        public string Description { get; } = description ?? string.Empty;
    }
}
