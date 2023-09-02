using ERP.Domain.Entities.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.Sales
{
    public class OrderDetailEntityProfile : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();

            builder.Property(x => x.ProductNumber).HasMaxLength(20);
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.SaleCategory).HasMaxLength(150);
        }
    }
}
