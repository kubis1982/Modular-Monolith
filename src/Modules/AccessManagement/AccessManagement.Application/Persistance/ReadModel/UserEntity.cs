namespace ModularMonolith.Modules.AccessManagement.Persistance.ReadModel
{
    using ModularMonolith.Modules.AccessManagement.Domain;
    using ModularMonolith.Shared.Kernel.Types;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Users", Schema = EntityType.ModuleCode)]
    public partial class UserEntity
    {
        [StringLength(5)]
        public EntityTypeId TypeId { get; set; } = null!;

        [Key]
        public int Id { get; set; }

        [StringLength(30)]
        public string? Email { get; set; }

        [StringLength(64)]
        public string Password { get; set; } = null!;

        public bool? IsActive { get; set; }

        [StringLength(20)]
        public string? FirstName { get; set; }

        [StringLength(20)]
        public string? MiddleName { get; set; }

        [StringLength(20)]
        public string? LastName { get; set; }
    }
}
