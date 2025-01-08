namespace ModularMonolith.Modules.Articles.Requests.Articles
{
    using ModularMonolith.Modules.Articles.Persistance;
    using System.ComponentModel.DataAnnotations;

    public class CreateArticleUnitRequest
    {
        /// <summary>
        /// Measurement unit name.
        /// </summary>
        [Required]
        [MaxLength(ArticleRestriction.UnitLength)]
        public string Unit { get; set; } = string.Empty;

        /// <summary>
        /// Measurement unit converter - numerator.
        /// </summary>
        [Required]
        public decimal Numerator { get; set; }

        /// <summary>
        /// Measurement unit converter - denominator.
        /// </summary>
        [Required]
        public decimal Denominator { get; set; }
    }
}
