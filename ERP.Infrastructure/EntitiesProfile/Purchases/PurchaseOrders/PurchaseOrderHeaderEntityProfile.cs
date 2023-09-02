using ERP.Domain.Entities.Purchases.PurchaseOrders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.Purchases.PurchaseOrders
{
    public class PurchaseOrderHeaderEntityProfile : IEntityTypeConfiguration<PurchaseOrderHeader>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderHeader> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();

            builder
                .HasMany(x => x.PurchaseOrderDetails)
                .WithOne(x => x.PurchaseOrderHeader)
                .HasForeignKey(x => x.PurchaseOrderHeaderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
