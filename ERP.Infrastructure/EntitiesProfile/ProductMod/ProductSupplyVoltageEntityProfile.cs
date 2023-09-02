using ERP.Domain.Entities.ProductMod;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.ProductMod
{
    public class ProductSupplyVoltageEntityProfile : IEntityTypeConfiguration<ProductSupplyVoltage>
    {
        public void Configure(EntityTypeBuilder<ProductSupplyVoltage> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();
        }
    }
}
