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

        public DateTime ExpirationDate { get; set; }

        [StringLength(UserRestriction.RefreshTokenLength)]
        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpirationDate { get; set; }

        public int? KilledBy { get; set; }
    }
}
