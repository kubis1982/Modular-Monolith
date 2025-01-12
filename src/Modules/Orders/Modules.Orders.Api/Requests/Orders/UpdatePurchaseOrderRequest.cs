namespace ModularMonolith.Modules.Orders.Requests.Orders
{
    using ModularMonolith.Modules.Orders.Persistance;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UpdatePurchaseOrderRequest
    {
        [Required]
        public int ContractorId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public AddressRequest? Address { get; set; } = new();

        [Required]
        public int WarehouseId { get; set; }

        [Required]
        public DateTime ExecutionDate { get; set; }

        [MaxLength(OrderRestriction.DescriptionLength)]
        public string? Description { get; set; }
    }
}
