namespace ModularMonolith.Modules.Ordering.Requests
{
    using ModularMonolith.Modules.Ordering.Persistance;
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
        public string? City { get; set; }

        [StringLength(OrderRestriction.AddressCountryLength)]
        public string? Country { get; set; }
    }
}
