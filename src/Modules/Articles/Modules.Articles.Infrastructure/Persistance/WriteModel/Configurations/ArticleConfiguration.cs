namespace ModularMonolith.Modules.Articles.Persistance.WriteModel.Configurations
{

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class ArticleConfiguration : DomainEntityTypeConfiguration<Article, ArticleId, EntityType>
    {

        protected override void OnConfigure(EntityTypeBuilder<Article> builder)
        {
            builder.Property(n => n.Code).HasConversion(n => n.Value, n => ArticleCode.Of(n)).HasMaxLength(ArticleRestriction.CodeLength).HasColumnOrder(100);
            builder.Property(n => n.Name).HasConversion(n => n.Value, n => ArticleName.Of(n)).HasMaxLength(ArticleRestriction.NameLength).HasColumnOrder(101);
            builder.Property(n => n.Unit).HasConversion(n => n.Value, n => MeasurementUnitCode.Of(n)).HasMaxLength(ArticleRestriction.UnitLength).HasColumnOrder(102);
            builder.Property(n => n.Description).HasMaxLength(ArticleRestriction.DescriptionLength).HasColumnOrder(103);
            builder.Property(n => n.IsBlocked).HasColumnOrder(104);
            builder.HasIndex(n => n.Code).IsUnique();
        }
    }
}
