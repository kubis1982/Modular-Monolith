namespace ModularMonolith.Modules.AccessManagement.Persistance.WriteModel
{
    using ModularMonolith.Modules.AccessManagement.Domain;
    using ModularMonolith.Modules.AccessManagement.Domain.Users;
    using ModularMonolith.Shared.Persistance.WriteModel;
    using Microsoft.EntityFrameworkCore;

    public class WriteDbContext(DbContextOptions<WriteDbContext> options) : WriteDbContextBase(options)
    {
        protected override string Schema => EntityType.ModuleCode;

        protected override void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(n =>
            {
                var administrator = User.Administrator;

                //n.HasData(new
                //{
                //    administrator.Id.TypeId,
                //    administrator.Id,
                //    administrator.Password,
                //    administrator.IsBlocked,
                //    CreatedBy = UserId.Administrator.Value,
                //    administrator.Email
                //});

                //n.OwnsOne(n => n.FullName).HasData(new { UserId = administrator.Id, administrator.FullName!.FirstName, administrator.FullName!.MiddleName, administrator.FullName!.LastName });
            });
        }
    }
}
