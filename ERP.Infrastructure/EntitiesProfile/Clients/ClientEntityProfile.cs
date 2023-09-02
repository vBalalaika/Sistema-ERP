using ERP.Domain.Entities.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.Clients
{
    public class ClientEntityProfile : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            // Reglas para cada campo en particular

            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();

            builder.Property(x => x.BusinessName).HasMaxLength(ConstantConfiguration.Name);
            builder.Property(x => x.BusinessName).IsRequired();
            builder.Property(x => x.CountryId).IsRequired();

            builder.Property(x => x.ClientDocument).HasMaxLength(255);

            builder.Property(x => x.SizeCompany).HasMaxLength(20);
            builder.Property(x => x.ProductionLevel).HasMaxLength(20);
            builder.Property(x => x.IndustryServed).HasMaxLength(100);
            builder.Property(x => x.BranchCompany).HasMaxLength(20);
            builder.Property(x => x.IsOnColppy).HasMaxLength(20);
            //builder.Property(x => x.Comments).HasMaxLength(500);

        }
    }
}
