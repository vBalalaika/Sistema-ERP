using ERP.Domain.Entities.Purchases.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.Purchases.Providers
{
    public class RelProviderStationEntityProfile : IEntityTypeConfiguration<RelProviderStation>
    {
        public void Configure(EntityTypeBuilder<RelProviderStation> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();
        }
    }
}
