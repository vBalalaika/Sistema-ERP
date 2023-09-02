using ERP.Domain.Entities.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.Infrastructure.EntitiesProfile.Sales
{
    public class PriceEntityProfile : IEntityTypeConfiguration<Price>
    {
        public void Configure(EntityTypeBuilder<Price> builder)
        {
            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();
            builder.Property(x => x.ProductId).IsRequired();
        }
    }
}
