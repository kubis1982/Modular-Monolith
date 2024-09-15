namespace ModularMonolith.Modules.AccessManagement.Persistance.ReadModel
{
    using ModularMonolith.Modules.AccessManagement.Domain;
    using ModularMonolith.Shared.Kernel.Types;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Users", Schema = EntityType.ModuleCode)]
    public partial class UserEntity
    {
        public EntityTypeId TypeId { get; set; } = null!;

        [Key]
        public int Id { get; set; }

        [StringLength(UserRestriction.EmailLength)]
        public string? Email { get; set; }

        [StringLength(UserRestriction.PasswordLength)]
        public string Password { get; set; } = null!;

        public bool? IsActive { get; set; }

        [StringLength(UserRestriction.FirstNameLength)]
        public string? FirstName { get; set; }

        [StringLength(UserRestriction.MiddleNameLength)]
        public string? MiddleName { get; set; }

        [StringLength(UserRestriction.LastNameLength)]
        public string? LastName { get; set; }
    }
}
