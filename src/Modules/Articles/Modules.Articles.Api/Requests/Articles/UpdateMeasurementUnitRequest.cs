namespace ModularMonolith.Modules.Articles.Requests.Articles
{
    using ModularMonolith.Modules.Articles.Persistance;
    using System.ComponentModel.DataAnnotations;

    public class UpdateMeasurementUnitRequest
    {
        /// <summary>
        /// Name of unit
        /// </summary>
        [Required]
        [MaxLength(MeasurementUnitRestriction.NameLength)]
        public string Name { get; set; } = string.Empty;
    }
}
