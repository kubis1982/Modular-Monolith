namespace ModularMonolith.Modules.Contractors.Requests
{
    using ModularMonolith.Modules.Contractors.Persistance;
    using ModularMonolith.Modules.Contractors.Requests.Common;
    using System.ComponentModel.DataAnnotations;

    public class CreateContractorRequest
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

        /// <summary>
        /// Country of contractor (ISO-3166-1 alfa 2)
        /// </summary>
        [Required]
        [StringLength(ContractorRestriction.CountryLength)]
        public string Country { get; set; } = "PL";

        [Required]
        public AddressRequest Address { get; set; } = null!;
    }
}
