namespace ModularMonolith.Modules.Contractors.Persistance.ReadModel
{
    using ModularMonolith.Modules.Contractors.Persistance.ReadModel.Entities;
    using ModularMonolith.Shared.Persistance.ReadModel;

    public class ReadDbContext : ReadDbContextBase
    {
        public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options)
        {
        }

        public virtual DbSet<ContractorEntity> Contractors { get; set; }
        public virtual DbSet<AddressEntity> Addresses { get; set; }
    }
}
