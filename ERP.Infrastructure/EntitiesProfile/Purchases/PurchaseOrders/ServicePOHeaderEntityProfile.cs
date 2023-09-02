using ERP.Domain.Entities.Purchases.PurchaseOrders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.Purchases.PurchaseOrders
{
    public class ServicePOHeaderEntityProfile : IEntityTypeConfiguration<ServicePOHeader>
    {
        public void Configure(EntityTypeBuilder<ServicePOHeader> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();
        }
    }
}
