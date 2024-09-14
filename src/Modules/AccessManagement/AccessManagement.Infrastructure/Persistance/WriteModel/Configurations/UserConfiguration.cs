namespace ModularMonolith.Modules.AccessManagement.Persistance.WriteModel.Configurations
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class UserConfiguration : DomainEntityTypeConfiguration<User, UserId, EntityType>
    {
        protected override void OnConfigure(EntityTypeBuilder<User> builder)
        {
            builder.Property(n => n.Email).HasConversion<string?>(n => n!.Value, n => UserEmail.Of(n)).HasMaxLength(UserRestriction.PasswordLength).HasMaxLength(UserRestriction.EmailLength).HasColumnOrder(7);
            builder.Property(n => n.Password).HasConversion(n => n.Hash, n => UserPassword.Of(n)).HasMaxLength(UserRestriction.PasswordLength).HasColumnOrder(8);
            builder.OwnsOne(n => n.FullName, n => {
                n.Property(m => m.FirstName).HasMaxLength(UserRestriction.FirstNameLength).HasColumnOrder(10).HasColumnName(nameof(UserFullName.FirstName));
                n.Property(m => m.MiddleName).HasMaxLength(UserRestriction.MiddleNameLength).HasColumnOrder(11).HasColumnName(nameof(UserFullName.MiddleName));
                n.Property(m => m.LastName).HasMaxLength(UserRestriction.LastNameLength).HasColumnOrder(12).HasColumnName(nameof(UserFullName.LastName));
            });
            
            builder.Property(n => n.IsActive);
            builder.HasIndex(n => n.Email).IsUnique();

            builder.HasMany(n => n.Sessions).WithOne().HasForeignKey(ShadowProperties.CreatedBy).OnDelete(DeleteBehavior.Restrict);

            builder.Navigation(n => n.Sessions).AutoInclude(false);
        }
    }
}
