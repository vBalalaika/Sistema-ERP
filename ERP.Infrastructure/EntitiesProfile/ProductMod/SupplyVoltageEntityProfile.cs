using ERP.Domain.Entities.ProductMod;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.ProductMod
{
    public class SupplyVoltageEntityProfile : IEntityTypeConfiguration<SupplyVoltage>
    {
        public void Configure(EntityTypeBuilder<SupplyVoltage> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();
        }
    }
}
