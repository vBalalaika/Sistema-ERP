using ERP.Domain.Entities.Purchases.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.Purchases.Providers
{
    public class ContactEntityProfile : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();

            builder.Property(x => x.Name).HasMaxLength(ConstantConfiguration.Name);
        }
    }
}
