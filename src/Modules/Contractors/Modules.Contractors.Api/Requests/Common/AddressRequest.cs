namespace ModularMonolith.Modules.Contractors.Requests.Common
{
    using ModularMonolith.Modules.Contractors.Persistance;
    using System.ComponentModel.DataAnnotations;

    public class AddressRequest
    {
        /// <summary>
        /// Line1
        /// </summary>
        [MaxLength(ContractorRestriction.AddressLineLength)]
        public string? Line1 { get; set; }
        /// <summary>
        /// Line2
        /// </summary>
        [MaxLength(ContractorRestriction.AddressLineLength)]
        public string? Line2 { get; set; }
        /// <summary>
        /// PostalCode
        /// </summary>
        [MaxLength(ContractorRestriction.AddressPostalCodeLength)]
        public string? PostalCode { get; set; }
        /// <summary>
        /// City
        /// </summary>
        [Required]
        [MaxLength(ContractorRestriction.AddressCityLength)]
        public string? City { get; set; }
        /// <summary>
        /// Country
        /// </summary>
        [Required]
        [MaxLength(ContractorRestriction.AddressCountryLength)]
        public string? Country { get; set; }
    }
}
