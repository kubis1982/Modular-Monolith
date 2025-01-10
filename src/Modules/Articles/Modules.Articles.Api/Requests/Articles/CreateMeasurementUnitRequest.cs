namespace ModularMonolith.Modules.Articles.Requests.Articles
{
    using ModularMonolith.Modules.Articles.Persistance;
    using System.ComponentModel.DataAnnotations;

    public class CreateMeasurementUnitRequest
    {
        /// <summary>
        /// Name of unit
        /// </summary>
        [Required]
        [MaxLength(MeasurementUnitRestriction.NameLength)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Code of unit
        /// </summary>
        [Required]
        [MaxLength(MeasurementUnitRestriction.CodeLength)]
        [RegularExpression(MeasurementUnitRestriction.CodePattern)]
        public string Code { get; set; } = string.Empty;
    }
}
