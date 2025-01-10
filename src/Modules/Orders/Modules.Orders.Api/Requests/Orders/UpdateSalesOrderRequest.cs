namespace ModularMonolith.Modules.Ordering.Requests.Orders
{
    using ModularMonolith.Modules.Ordering.Persistance;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UpdateSalesOrderRequest
    {
        [Required]
        public int ContractorId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public AddressRequest? Address { get; set; }

        [Required]
        public int WarehouseId { get; set; }

        [Required]
        public DateTime ExecutionDate { get; set; }

        [MaxLength(OrderRestriction.DescriptionLength)]
        public string? Description { get; set; }
    }
}
