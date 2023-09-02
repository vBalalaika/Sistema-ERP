using ERP.Domain.Entities.Purchases.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.Purchases.Providers
{
    public class ProviderEntityProfile : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();

            builder.Property(x => x.Name).HasMaxLength(ConstantConfiguration.Name);
            builder.Property(x => x.BusinessName).HasMaxLength(ConstantConfiguration.Name).IsRequired();

            builder.Property(x => x.DocumentTypeId).IsRequired();

            builder.Property(x => x.IVAConditionId).IsRequired();

            builder.Property(x => x.CountryId).IsRequired();

            builder.Property(x => x.ProviderType).HasMaxLength(150);
            //builder.Property(x => x.Observations).HasMaxLength(ConstantConfiguration.Name);

            builder
                .HasMany(x => x.Contacts)
                .WithOne(x => x.Provider)
                .HasForeignKey(x => x.ProviderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(x => x.Subsidiaries)
                .WithOne(x => x.Provider)
                .HasForeignKey(x => x.ProviderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(x => x.FinancialInformations)
                .WithOne(x => x.Provider)
                .HasForeignKey(x => x.ProviderId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
