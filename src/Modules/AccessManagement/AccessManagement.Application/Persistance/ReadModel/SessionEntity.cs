namespace ModularMonolith.Modules.AccessManagement.Persistance.ReadModel
{
    using ModularMonolith.Modules.AccessManagement.Domain;
    using ModularMonolith.Shared.Kernel.Types;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Sessions", Schema = EntityType.ModuleCode)]
    public partial class SessionEntity
    {
        [StringLength(5)]
        public EntityTypeId TypeId { get; set; } = null!;

        [Key]
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }

        public DateTime ExpiryDate { get; set; }

        [StringLength(256)]
        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryDate { get; set; }

        public int? KilledBy { get; set; }
    }
}
