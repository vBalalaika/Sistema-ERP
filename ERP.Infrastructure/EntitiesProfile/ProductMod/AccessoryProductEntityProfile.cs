using ERP.Domain.Entities.ProductMod;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.ProductMod
{
    public class AccessoryProductEntityProfile : IEntityTypeConfiguration<AccessoryProduct>
    {
        public void Configure(EntityTypeBuilder<AccessoryProduct> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();
        }
    }
}
