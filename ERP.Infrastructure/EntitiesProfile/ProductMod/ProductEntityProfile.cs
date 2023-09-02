using ERP.Domain.Entities.ProductMod;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.ProductMod
{
    public class ProductEntityProfile : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();

            builder.Property(x => x.Code).HasMaxLength(ConstantConfiguration.Code);
            builder.Property(x => x.Description).HasMaxLength(ConstantConfiguration.ProductDescription);
            builder.Property(x => x.Observation).HasMaxLength(ConstantConfiguration.Text);
        }
    }
}
