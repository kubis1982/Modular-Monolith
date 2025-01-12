namespace ModularMonolith.Modules.Orders.Requests
{
    using ModularMonolith.Modules.Orders.Persistance;
    using System.ComponentModel.DataAnnotations;

    public class AddressRequest
    {
        [StringLength(OrderRestriction.AddressLineLength)]
        public string? Line1 { get; set; }

        [StringLength(OrderRestriction.AddressLineLength)]
        public string? Line2 { get; set; }

        [StringLength(OrderRestriction.AddressPostalCodeLength)]
        public string? PostalCode { get; set; }

        [StringLength(OrderRestriction.AddressCityLength)]
        [Required]
        public string? City { get; set; }

        [StringLength(OrderRestriction.AddressCountryLength)]
        [Required]
        public string? Country { get; set; }
    }
}
