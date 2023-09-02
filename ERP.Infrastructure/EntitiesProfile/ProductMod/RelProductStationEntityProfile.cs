using ERP.Domain.Entities.ProductMod;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.ProductMod
{
    public class RelProductStationEntityProfile : IEntityTypeConfiguration<RelProductStation>
    {
        public void Configure(EntityTypeBuilder<RelProductStation> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();
        }
    }
}
