namespace ModularMonolith.Modules.Ordering.Requests.Orders
{
    using ModularMonolith.Modules.Ordering.Persistance;
    using System.ComponentModel.DataAnnotations;

    public class CreatePurchaseItemRequest
    {
        [Required]
        public int ArticleId { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        [Required]
        [MaxLength(OrderRestriction.UnitLength)]
        public string Unit { get; set; } = string.Empty;

        public int Numerator { get; set; } = 1;

        public int Denominator { get; set; } = 1;

        [MaxLength(OrderRestriction.DescriptionLength)]
        public string? Description { get; set; }
    }
}
