using ERP.Domain.Entities.ProductMod;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ERP.Infrastructure.EntitiesProfile.ProductMod
{
    public class ProductFeatureEntityProfile : IEntityTypeConfiguration<ProductFeature>
    {
        public void Configure(EntityTypeBuilder<ProductFeature> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();
            builder.Property(x => x.RawMaterialCode).HasMaxLength(ConstantConfiguration.Code);
            builder.Property(x => x.RawMaterialDescription).HasMaxLength(ConstantConfiguration.Description);

        }
    }
}
