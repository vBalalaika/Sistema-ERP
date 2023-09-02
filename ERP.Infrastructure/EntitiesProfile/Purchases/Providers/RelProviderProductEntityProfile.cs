using ERP.Domain.Entities.Purchases.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.Purchases.Providers
{
    public class RelProviderProductEntityProfile : IEntityTypeConfiguration<RelProviderProduct>
    {
        public void Configure(EntityTypeBuilder<RelProviderProduct> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();
        }
    }
}
