using ERP.Domain.Entities.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.Sales
{
    public class OrderHeaderEntityProfile : IEntityTypeConfiguration<OrderHeader>
    {
        public void Configure(EntityTypeBuilder<OrderHeader> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();

            // Se le setea el valor del Id
            builder.Property(x => x.Number).HasComputedColumnSql("[Id]");
            builder.Property(x => x.SaleObservations).HasMaxLength(500);
            builder.Property(x => x.ProductionObservations).HasMaxLength(500);

            builder.Property(x => x.Packaging).HasMaxLength(255);
            builder.Property(x => x.PaymentMethod).HasMaxLength(255);
            builder.Property(x => x.PlaceOfDelivery).HasMaxLength(255);
            builder.Property(x => x.Transport).HasMaxLength(255);
            builder.Property(x => x.TypeOfFreightAndSecure).HasMaxLength(255);

            builder.Property(x => x.User).HasMaxLength(250);
        }
    }
}
