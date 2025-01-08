namespace ModularMonolith.Modules.Articles.Requests.Articles
{
    using ModularMonolith.Modules.Articles.Persistance;
    using System.ComponentModel.DataAnnotations;

    public class CreateArticleRequest
    {
        /// <summary>
        /// Code of article.
        /// </summary>
        [Required]
        [MaxLength(ArticleRestriction.CodeLength)]
        [RegularExpression(ArticleRestriction.CodePattern)]
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// Name of article.
        /// </summary>
        [Required]
        [MaxLength(ArticleRestriction.NameLength)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Unit of article.
        /// </summary>
        [Required]
        [MaxLength(ArticleRestriction.UnitLength)]
        public string Unit { get; set; } = string.Empty;

        /// <summary>
        /// Description of article
        /// </summary>
        [MaxLength(ArticleRestriction.DescriptionLength)]
        public string? Description { get; set; }
    }
}
