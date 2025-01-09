namespace ModularMonolith.Modules.Contractors.Requests
{
    using ModularMonolith.Modules.Contractors.Persistance;
    using ModularMonolith.Modules.Contractors.Requests.Common;
    using System.ComponentModel.DataAnnotations;

    public class UpdateContractorRequest
    {
        /// <summary>
        /// Name of contractor.
        /// </summary>
        [Required]
        public required string Name { get; set; }

        /// <summary>
        /// Code of contractor.
        /// </summary>
        [Required]
        [RegularExpression(ContractorRestriction.CodePattern)]
        public required string Code { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string? Description { get; set; }

        [Required]
        [StringLength(ContractorRestriction.CountryLength)]
        public string Country { get; set; } = "PL";

        [Required]
        public UpdateContractorAddressRequest Address { get; set; } = null!;
    }

    public class UpdateContractorAddressRequest : AddressRequest
    {
        /// <summary>
        /// Address id.
        /// </summary>
        [Required]
        public int Id { get; set; }
    }
}
