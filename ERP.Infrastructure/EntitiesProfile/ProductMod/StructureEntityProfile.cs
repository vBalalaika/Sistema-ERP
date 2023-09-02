using ERP.Domain.Entities.ProductMod;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.ProductMod
{
    public class StructureEntityProfile : IEntityTypeConfiguration<Structure>
    {
        public void Configure(EntityTypeBuilder<Structure> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();
            builder.Property(x => x.Description).HasMaxLength(ConstantConfiguration.Description);
        }
    }
}
