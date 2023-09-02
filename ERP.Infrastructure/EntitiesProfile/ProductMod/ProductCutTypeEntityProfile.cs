using ERP.Domain.Entities.ProductMod;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.ProductMod
{
    public class ProductCutTypeEntityProfile : IEntityTypeConfiguration<ProductCutType>
    {
        public void Configure(EntityTypeBuilder<ProductCutType> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();
        }

    }
}
