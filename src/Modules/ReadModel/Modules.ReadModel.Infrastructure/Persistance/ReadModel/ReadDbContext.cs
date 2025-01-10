namespace ModularMonolith.Modules.ReadModel.Persistance.ReadModel
{
    using Microsoft.EntityFrameworkCore;
    using ModularMonolith.Modules.ReadModel.Persistance.ReadModel.Articles;
    using ModularMonolith.Modules.ReadModel.Persistance.ReadModel.Contractors;
    using ModularMonolith.Modules.ReadModel.Persistance.ReadModel.Entities;
    using ModularMonolith.Modules.ReadModel.Persistance.ReadModel.Warehouses;
    using ModularMonolith.Shared.Persistance.ReadModel;

#nullable disable

    public partial class ReadDbContext(DbContextOptions<ReadDbContext> options) : ReadDbContextBase(options) {
        #region ACCESS CONTROL

        public virtual DbSet<UserEntity> Users { get; set; }

        #endregion

        #region ARTICLES

        public virtual DbSet<ArticleEntity> Articles { get; set; }

        #endregion

        #region CONTRACTORS

        public virtual DbSet<ContractorEntity> Contractors { get; set; }
        public virtual DbSet<ContractorAddressEntity> ContractorAddresses { get; set; }

        #endregion

        #region WAREHOUSES

        public virtual DbSet<WarehouseEntity> Warehouses { get; set; }

        #endregion

        #region ORDERING

        public virtual DbSet<Ordering.OrderEntity> Orders { get; set; }
        public virtual DbSet<Ordering.OrderItemEntity> OrderItems { get; set; }

        #endregion
    }
}
