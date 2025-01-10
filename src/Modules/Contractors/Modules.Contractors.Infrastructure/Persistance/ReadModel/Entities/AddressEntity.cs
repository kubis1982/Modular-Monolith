namespace ModularMonolith.Modules.Contractors.Persistance.ReadModel.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Addresses", Schema = EntityType.ModuleCode)]
    public class AddressEntity
    {
        [StringLength(5)]
        public string TypeId { get; set; } = null!;

        [Key]
        public int Id { get; set; }

        [Precision(2)]
        public DateTime? CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        [Precision(2)]
        public DateTime? ModifiedOn { get; set; }

        public int? ModifiedBy { get; set; }

        public int? ContractorId { get; set; }

        public string? Line1 { get; set; }
        public string? Line2 { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public bool IsDefault { get; set; }
    }
}
