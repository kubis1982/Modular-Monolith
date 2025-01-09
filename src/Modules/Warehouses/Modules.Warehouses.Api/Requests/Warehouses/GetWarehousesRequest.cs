namespace ModularMonolith.Modules.Warehouses.Requests.Warehouses
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Represents a request to get articles.
    /// </summary>
    public class GetWarehousesRequest
    {
        /// <summary>
        /// Gets or sets a value indicating whether to include blocked articles.
        /// </summary>
        [FromQuery]
        public bool? IncludeBlocked { get; set; }
    }
}
