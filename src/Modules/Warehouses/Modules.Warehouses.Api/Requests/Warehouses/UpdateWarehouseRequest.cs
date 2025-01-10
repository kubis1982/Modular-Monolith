namespace ModularMonolith.Modules.Warehouses.Requests.Warehouses
{
    using ModularMonolith.Modules.Warehouses.Persistance;
    using System.ComponentModel.DataAnnotations;

    public class UpdateWarehouseRequest
    {
        [Required]
        [MaxLength(WarehouseRestriction.CodeLength)]
        [RegularExpression(WarehouseRestriction.CodePattern)]
        public required string Code { get; set; }

        [Required]
        [MaxLength(WarehouseRestriction.NameLength)]
        public required string Name { get; set; }

        [MaxLength(WarehouseRestriction.DescriptionLength)]
        public string? Description { get; set; }
    }
}
