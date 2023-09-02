using ERP.Domain.Entities.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.Commons
{
    public class UnitMeasureEntityProfile : IEntityTypeConfiguration<UnitMeasure>
    {
        public void Configure(EntityTypeBuilder<UnitMeasure> builder)
        {
            builder.Property(x => x.Description).HasMaxLength(255);
            builder.Property(x => x.Abbreviation).HasMaxLength(50);
        }
    }
}
