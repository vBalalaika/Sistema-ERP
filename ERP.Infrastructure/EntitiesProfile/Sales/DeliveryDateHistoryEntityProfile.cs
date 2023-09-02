using ERP.Domain.Entities.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.Sales
{
    public class DeliveryDateHistoryEntityProfile : IEntityTypeConfiguration<DeliveryDateHistory>
    {
        public void Configure(EntityTypeBuilder<DeliveryDateHistory> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();
        }
    }
}
