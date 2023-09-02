using ERP.Domain.Entities.Purchases.PurchaseOrders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.Purchases.PurchaseOrders
{
    public class ServicePODetailEntityProfile : IEntityTypeConfiguration<ServicePODetail>
    {
        public void Configure(EntityTypeBuilder<ServicePODetail> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();
        }
    }
}
