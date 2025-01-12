namespace ModularMonolith.Modules.Orders.Requests.Orders
{
    using ModularMonolith.Modules.Orders.Persistance;
    using System.ComponentModel.DataAnnotations;

    public class UpdateSalesItemRequest
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
