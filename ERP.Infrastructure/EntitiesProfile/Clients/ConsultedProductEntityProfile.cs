using ERP.Domain.Entities.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.Clients
{
    public class ConsultedProductEntityProfile : IEntityTypeConfiguration<ConsultedProduct>
    {
        public void Configure(EntityTypeBuilder<ConsultedProduct> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();
        }
    }
}
