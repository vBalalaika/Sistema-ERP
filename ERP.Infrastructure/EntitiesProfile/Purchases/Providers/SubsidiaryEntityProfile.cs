using ERP.Domain.Entities.Purchases.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.Purchases.Providers
{
    public class SubsidiaryEntityProfile : IEntityTypeConfiguration<Subsidiary>
    {
        public void Configure(EntityTypeBuilder<Subsidiary> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();

            builder.Property(x => x.CountryId).IsRequired();

        }
    }
}
